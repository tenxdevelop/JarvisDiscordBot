/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using Discord.WebSocket;
using Discord.Commands;
using Discord;

namespace JarvisDiscordBot.Models
{
    public class DiscordBotModel : IDiscordBotModel
    {
        public DiscordSocketClient DiscordClient { get; private set; }

        public CommandService DiscordCommand { get; private set; }

        public string Token { get; private set; }

        public string Prefix { get; private set; }

        public DiscordBotModel(Config config)
        {
            Token = config.Token;
            Prefix = config.Prefix;

            var discordClientConfig = new DiscordSocketConfig()
            {
                AlwaysDownloadUsers = true,
                MessageCacheSize = 100,
                LogLevel = LogSeverity.Debug
            };

            DiscordClient = new DiscordSocketClient(discordClientConfig);

            var discordCommandConfig = new CommandServiceConfig()
            {
                CaseSensitiveCommands = false,
                LogLevel = LogSeverity.Debug
            };

            DiscordCommand = new CommandService(discordCommandConfig);
        }
    }
}
