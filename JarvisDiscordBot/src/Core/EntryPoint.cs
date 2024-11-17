/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using JarvisDiscordBot.Services.Deserializer;
using JarvisDiscordBot.ViewModels;
using JarvisDiscordBot.Services;
using JarvisDiscordBot.Models;
using System.Threading.Tasks;
using System;


namespace JarvisDiscordBot.Core
{
    public class EntryPoint : IDisposable
    {
        private IDiscordBotViewModel m_discordBot;
        public async Task Start()
        {
            FileSystem.Init<NetCoreIOController>();
            Log.Init();

            var deserializer = new Deserializer();
            deserializer.Init<JSONDeserializer>();

            var config = await deserializer.ReadConfig();

            var discordBotModel = new DiscordBotModel(config);
            m_discordBot = new DiscordBotViewModel(discordBotModel);

            DiscordCommandRegistration.RegisterCommand(m_discordBot);

            await m_discordBot.Start();
        }

        public void Dispose()
        {
            Log.CoreLogger?.Logging("Destroy system.", LogLevel.Info);
            Log.Destroy();
        }
        
    }
}
