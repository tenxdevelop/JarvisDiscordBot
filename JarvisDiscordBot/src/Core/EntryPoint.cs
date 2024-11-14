/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using JarvisDiscordBot.Services.Deserializer;
using JarvisDiscordBot.Services;
using DSharpPlus.CommandsNext;
using System.Threading.Tasks;
using DSharpPlus.EventArgs;
using DSharpPlus;
using System;


namespace JarvisDiscordBot.Core
{
    public class EntryPoint : IDisposable
    {
        private bool m_disposed = false;

        private DiscordClient m_discordClient;
        private CommandsNextExtension m_commandsExtension;
        public async Task Start()
        {
            FileSystem.Init<NetCoreIOController>();
            Log.Init();

            var deserializer = new Deserializer();
            deserializer.Init<JSONDeserializer>();

            var config = await deserializer.ReadConfig();

            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = config.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            m_discordClient = new DiscordClient(discordConfig);
            m_discordClient.Ready += ClientReady;
            await m_discordClient.ConnectAsync();
            await Task.Delay(-1);
        }

        private Task ClientReady(DiscordClient sender, ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Log.CoreLogger?.Logging("Destroy system.", LogLevel.Info);
            Log.Destroy();
        }
     
    }
}
