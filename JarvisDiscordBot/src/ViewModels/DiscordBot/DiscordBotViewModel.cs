/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using JarvisDiscordBot.Models;
using System.Threading.Tasks;

namespace JarvisDiscordBot.ViewModels
{
    public sealed class DiscordBotViewModel : IDiscordBotViewModel
    {
        private DiscordBotModel m_discordBotModel;
        public DiscordBotViewModel(DiscordBotModel discordBotModel)
        {
            m_discordBotModel = discordBotModel;
        }

        public void RegisterCommand<TCommand>() where TCommand : IDiscordCommandModel
        {
            var typeCommand = typeof(TCommand);

            m_discordBotModel.DiscordCommand.RegisterCommands(typeCommand);
        }

        public async Task Start()
        {
            await m_discordBotModel.DiscordClient.ConnectAsync();
            await Task.Delay(-1);
        }

       
    }
}
