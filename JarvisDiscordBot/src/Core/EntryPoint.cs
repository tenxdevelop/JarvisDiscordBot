/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using Microsoft.Extensions.DependencyInjection;
using JarvisDiscordBot.Services.Deserializer;
using JarvisDiscordBot.ViewModels;
using JarvisDiscordBot.Services;
using JarvisDiscordBot.Models;

namespace JarvisDiscordBot.Core
{
    public class EntryPoint : IDisposable
    {
        private IDiscordBotViewModel? m_discordBot;
        private ServiceCollection? m_rootContainer;
        public async Task Start()
        {
            FileSystem.Init<NetCoreIOController>();
            Log.Init();
            m_rootContainer = new ServiceCollection();

            var deserializer = new Deserializer();
            deserializer.Init<JSONDeserializer>();

            var config = await deserializer.ReadConfig();
            m_rootContainer.AddSingleton(factory => config);

            var container = RegisterService(m_rootContainer);

            var discordBotModel = new DiscordBotModel(container, config);
            m_discordBot = new DiscordBotViewModel(discordBotModel);

            DiscordCommandRegistration.RegisterCommand(m_discordBot);

            await m_discordBot.Start();
        }

        public void Dispose()
        {
            Log.CoreLogger?.Logging("Destroy system.", LogLevel.Info);
            Log.Destroy();
        }

        private ServiceProvider RegisterService(ServiceCollection container)
        {
            container.AddSingleton<IVkAudioService>(factory => new VkAudioService(factory.GetRequiredService<Config>()));
            container.AddSingleton<IYoutubeService>(factory => new YoutubeService(factory.GetRequiredService<Config>()));
            return container.BuildServiceProvider();
        }
    }
}
