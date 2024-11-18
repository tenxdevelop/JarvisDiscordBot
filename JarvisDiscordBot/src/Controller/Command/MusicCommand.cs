/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using DSharpPlus.CommandsNext.Attributes;
using JarvisDiscordBot.Services;
using DSharpPlus.CommandsNext;
using JarvisDiscordBot.Models;
using DSharpPlus.Lavalink;
using DSharpPlus.Entities;
using DSharpPlus;

namespace JarvisDiscordBot.Controller
{
    public sealed class MusicCommand : BaseCommandModule, IDiscordCommandModel
    {
        private const string CHANEL_MUSIC = "music";
        private VkAudioService m_vkAudioService;

        public MusicCommand(VkAudioService vkAudioService)
        {
            m_vkAudioService = vkAudioService;
        }

        [Command("play")]
        public async Task PlayMusicInfoOrResume(CommandContext commandContext)
        {
            var isNotCorrectChannel = await CheckChannel(commandContext);
   
            if (isNotCorrectChannel)
               return;

            var lavalink = commandContext.Client.GetLavalink();
            var userVoiceChannel = commandContext.Member?.VoiceState.Channel;

            if (lavalink.ConnectedNodes.Any() && userVoiceChannel is not null && commandContext.Member?.VoiceState is not null)
            {
                var node = lavalink.ConnectedNodes.Values.First();
                var connection = node.GetGuildConnection(commandContext.Member.VoiceState.Guild);
                if (connection.CurrentState.CurrentTrack is not null)
                {
                    await connection.ResumeAsync();

                    var resumeEmbed = new DiscordEmbedBuilder()
                    {
                        Color = DiscordColor.Green,
                        Title = $"Трек {connection.CurrentState.CurrentTrack.Title} возабновлен.",
                    };

                    await commandContext.Channel.SendMessageAsync(embed: resumeEmbed);

                    return;
                }
            }

            await commandContext.Channel.SendMessageAsync("введите название песни через пробел.");
        }

        [Command("pause")]
        public async Task PauseMusic(CommandContext commandContext)
        {
            var isNotCorrectChannel = await CheckChannel(commandContext);

            if (isNotCorrectChannel)
                return;

            var connection = await GetConnection(commandContext);

            await connection.PauseAsync();

            var pausedEmbed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Yellow,
                Title = "Музыка приостановлена."
            };

            await commandContext.Channel.SendMessageAsync(embed: pausedEmbed);
        }

        [Command("play")]
        public async Task PlayMusicFromYoutube(CommandContext commandContext, [RemainingText] string query)
        {

            await PlayMusic(commandContext, query, SearchMusicType.YoutubeWithMusicName);
        }

        [Command("playVk")]
        public async Task PlayMusicFromVk(CommandContext commandContext, [RemainingText] string query)
        {
            await PlayMusic(commandContext, query, SearchMusicType.VkWithMusicName);
        }

        [Command("playUrl")]
        public async Task PlayMusicFromYoutubeWithUrl(CommandContext commandContext, [RemainingText] string query)
        {
            await PlayMusic(commandContext, query, SearchMusicType.YoutubeWithMusicUrl);
        }

        [Command("stop")]
        public async Task StopMusic(CommandContext commandContext)
        {
            var isNotCorrectChannel = await CheckChannel(commandContext);

            if (isNotCorrectChannel)
                return;

            var connection = await GetConnection(commandContext);

            await connection.StopAsync();
            await connection.DisconnectAsync();

            var stopEmbed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Red,
                Title = $"Трек {connection.CurrentState.CurrentTrack.Title} приостановлен.",
                Description = "Успешно вышел из голосового сервера!"
            };

            await commandContext.Channel.SendMessageAsync(embed: stopEmbed);
        }

        private async Task PlayMusic(CommandContext commandContext, string query, SearchMusicType searchMusicType)
        {
            var isNotCorrectChannel = await CheckChannel(commandContext);

            if (isNotCorrectChannel)
                return;

            var userVoiceChannel = commandContext.Member?.VoiceState.Channel;
            var lavalink = commandContext.Client.GetLavalink();

            if (await UserIsNotConnectToVoiceChannel(commandContext, userVoiceChannel))
                return;

            if (await LavalinkIsNotConnect(commandContext, lavalink))
                return;

            var node = lavalink.ConnectedNodes.Values.First();
            await node.ConnectAsync(userVoiceChannel);

            var connection = node.GetGuildConnection(commandContext.Member?.VoiceState.Guild);

            if (connection is null)
            {
                await commandContext.Channel.SendMessageAsync("Соединение не установлено....");
                Log.CoreLogger?.Logging("Error can't connection lavalink to discord server!!", LogLevel.Error);
                return;
            }

            LavalinkTrack music = null;

            switch (searchMusicType)
            {
                case SearchMusicType.YoutubeWithMusicName:
                    var searchQuery = await node.Rest.GetTracksAsync(query, LavalinkSearchType.Youtube);

                    if (searchQuery.LoadResultType == LavalinkLoadResultType.NoMatches ||
                        searchQuery.LoadResultType == LavalinkLoadResultType.LoadFailed)
                    {
                        await commandContext.Channel.SendMessageAsync($"Не уддалось найти музыку: {query} на Youtube");
                        Log.ClientLogger?.Logging($"Can't found music by query: {query}", LogLevel.Info);
                        return;
                    }

                    music = searchQuery.Tracks.First();
                    break;

                case SearchMusicType.YoutubeWithMusicUrl:
                    searchQuery = await node.Rest.GetTracksAsync(new Uri(query));
                    if (searchQuery.LoadResultType == LavalinkLoadResultType.NoMatches ||
                        searchQuery.LoadResultType == LavalinkLoadResultType.LoadFailed)
                    {
                        await commandContext.Channel.SendMessageAsync($"Не уддалось найти музыку: {query} на Youtube");
                        Log.ClientLogger?.Logging($"Can't found music by query: {query}", LogLevel.Info);
                        return;
                    }

                    music = searchQuery.Tracks.First();
                    break;

                case SearchMusicType.VkWithMusicName:
                    var audioUrl = await m_vkAudioService.FindAudioUrlByNameAsync(query);
                    searchQuery = await node.Rest.GetTracksAsync(audioUrl);
                    if (searchQuery.LoadResultType == LavalinkLoadResultType.NoMatches ||
                        searchQuery.LoadResultType == LavalinkLoadResultType.LoadFailed)
                    {
                        await commandContext.Channel.SendMessageAsync($"Не уддалось найти музыку: {query} на Lavalink");
                        Log.ClientLogger?.Logging($"Can't found music by query: {query}", LogLevel.Info);
                        return;
                    }

                    music = searchQuery.Tracks.First();
                    break;
            }
            
            await connection.PlayAsync(music);
            Log.ClientLogger?.Logging($"Connect to channel: {userVoiceChannel.Name}", LogLevel.Info);

            await ShowMusicDescription(commandContext, music, userVoiceChannel.Name);
        }

        private async Task<bool> CheckChannel(CommandContext commandContext)
        {
            var result = !commandContext.Channel.Name.Equals(CHANEL_MUSIC);
            if (result)
            {
                var user = commandContext.Member;
                await user.CreateDmChannelAsync();
                await user.SendMessageAsync("для воспроизведения музыки, отправляите запрос в канал music!");
                await commandContext.Message.DeleteAsync();
            }

            return result;
        }

        private async Task<bool> UserIsNotConnectToVoiceChannel(CommandContext commandContext, DiscordChannel userVoiceChannel)
        {
            var result = commandContext.Member?.VoiceState is null || userVoiceChannel is null || userVoiceChannel.Type != ChannelType.Voice;
            if (result)
                await commandContext.Channel.SendMessageAsync("Пожалуйста зайдите в любой голосовой канал.");

            return result;
        }

        private async Task<bool> LavalinkIsNotConnect(CommandContext commandContext, LavalinkExtension lavalink)
        {
            var result = !lavalink.ConnectedNodes.Any();
            if (result)
            {
                await commandContext.Channel.SendMessageAsync("Соединение не установлено....");
                Log.CoreLogger?.Logging("Error can't connection lavalink!!", LogLevel.Error);                
            }
            return result;
        }

        private async Task<LavalinkGuildConnection> GetConnection(CommandContext commandContext)
        {
            var userVoiceChannel = commandContext.Member?.VoiceState.Channel;
            var lavalink = commandContext.Client.GetLavalink();

            if (await UserIsNotConnectToVoiceChannel(commandContext, userVoiceChannel))
                return null;

            if (await LavalinkIsNotConnect(commandContext, lavalink))
                return null;

            var node = lavalink.ConnectedNodes.Values.First();
            var connection = node.GetGuildConnection(commandContext.Member?.VoiceState.Guild);

            if (connection is null)
            {
                await commandContext.Channel.SendMessageAsync("Соединение не установлено...");
                Log.CoreLogger?.Logging("Error can't find lavalink in discord server", LogLevel.Error);
                return null;
            }

            if (connection.CurrentState.CurrentTrack == null)
            {
                await commandContext.Channel.SendMessageAsync("Музыка уже не играет!");
                return null;
            }
            return connection;
        }

        private async Task ShowMusicDescription(CommandContext commandContext, LavalinkTrack music, string ChannelName)
        {
            var musicDescription = $"Сейчас играет: {music.Title} \n" + $"Автор: {music.Author} \n";
            var musicDescriptionEmbed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Cyan,
                Title = $"подключился к каналу {ChannelName}",
                Description = musicDescription
            };
            await commandContext.Channel.SendMessageAsync(musicDescriptionEmbed);
        }

    }

}
