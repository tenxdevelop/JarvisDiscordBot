/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using JarvisDiscordBot.Models;

namespace JarvisDiscordBot.Controller
{
    public sealed class MusicCommand : IDiscordCommandModel
    {
        private const string CHANEL_MUSIC = "music";

        //[Command("play")]
        //public async Task PlayMusicInfo(CommandContext commandContext)
        //{
        //    var isNotCorrectChannel = await CheckChannel(commandContext);

        //    if (isNotCorrectChannel)
        //        return;

        //    await commandContext.Channel.SendMessageAsync("введите название песни через пробел.");
        //}

        //[Command("play")]
        //public async Task PlayMusic(CommandContext commandContext, [RemainingText] string query)
        //{
        //    var userVoiceChanel = commandContext.Member.VoiceState.Channel;
        //    var lavalink = commandContext.Client.GetLavalink();

        //    var isNotCorrectChannel = await CheckChannel(commandContext);

        //    if (isNotCorrectChannel)
        //        return;

        //    if (commandContext.Member.VoiceState is null || userVoiceChanel is null || userVoiceChanel.Type != ChannelType.Voice)
        //    {
        //        await commandContext.Channel.SendMessageAsync("Пожалуйста зайдите в любой голосовой канал.");
        //        return;
        //    }

        //    if (!lavalink.ConnectedNodes.Any())
        //    {
        //        await commandContext.Channel.SendMessageAsync("Соединение не установлено....");
        //        Log.CoreLogger?.Logging("Error can't connection lavalink!!", LogLevel.Error);
        //        return;
        //    }

        //    var node = lavalink.ConnectedNodes.Values.First();
        //    await node.ConnectAsync(userVoiceChanel);
        //    var connection = node.GetGuildConnection(commandContext.Member.VoiceState.Guild);

        //    if (connection is null)
        //    {
        //        await commandContext.Channel.SendMessageAsync("Соединение не установлено....");
        //        Log.CoreLogger?.Logging("Error can't connection lavalink!!", LogLevel.Error);
        //        return;
        //    }

        //    var searchQuery = await node.Rest.GetTracksAsync(query, LavalinkSearchType.Youtube);

        //    if (searchQuery.LoadResultType == LavalinkLoadResultType.NoMatches ||
        //        searchQuery.LoadResultType == LavalinkLoadResultType.LoadFailed)
        //    {
        //        await commandContext.Channel.SendMessageAsync($"Не уддалось найти музыку: {query} на youtube");
        //        Log.ClientLogger?.Logging($"Can't found music by query: {query}", LogLevel.Info);
        //        return;
        //    }

        //    var music = searchQuery.Tracks.First();

        //    await connection.PlayAsync(music);
        //    Log.ClientLogger?.Logging($"Connect to channel: {userVoiceChanel.Name}", LogLevel.Info);

        //    await ShowMusicDescription(commandContext, music, userVoiceChanel.Name);
        //}


        //private async Task<bool> CheckChannel(CommandContext commandContext)
        //{
        //    var result = !commandContext.Channel.Name.Equals(CHANEL_MUSIC);
        //    if (result)
        //    {
        //        var user = commandContext.Member;
        //        await user.CreateDmChannelAsync();
        //        await user.SendMessageAsync("для воспроизведения музыки, отправляите запрос в канал music!");
        //        await commandContext.Message.DeleteAsync();
        //    }

        //    return result;
        //}

        //private async Task ShowMusicDescription(CommandContext commandContext, LavalinkTrack music, string ChannelName)
        //{
        //    var musicDescription = $"Сейчас играет: {music.Title} \n" + $"Автор: {music.Author} \n";
        //    var musicDescriptionEmbed = new DiscordEmbedBuilder()
        //    {
        //        Color = DiscordColor.Cyan,
        //        Title = $"подключился к каналу {ChannelName}",
        //        Description = musicDescription
        //    };
        //    await commandContext.Channel.SendMessageAsync(musicDescriptionEmbed);
        //}
    }
}
