/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using DSharpPlus.CommandsNext;
using System.Threading.Tasks;
using DSharpPlus.EventArgs;
using DSharpPlus;

namespace JarvisDiscordBot.Models
{
    public class DiscordBotModel
    {
        public DiscordClient DiscordClient { get; private set; }
        public CommandsNextExtension DiscordCommand { get; private set; }

        public DiscordBotModel(Config config) 
        {
            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = config.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            DiscordClient = new DiscordClient(discordConfig);
            DiscordClient.Ready += ClientReady;

            var commandConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { config.Prefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false
            };

            DiscordCommand = DiscordClient.UseCommandsNext(commandConfig);
        }

        private Task ClientReady(DiscordClient sender, ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
