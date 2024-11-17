/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using JarvisDiscordBot.Models;

namespace JarvisDiscordBot.ViewModels
{
    public sealed class DiscordBotViewModel : IDiscordBotViewModel
    {
        private IDiscordBotModel m_discordBotModel;
        public DiscordBotViewModel(IDiscordBotModel discordBotModel)
        {
            m_discordBotModel = discordBotModel;
        }

        public void RegisterCommand<TCommand>() where TCommand : IDiscordCommandModel
        {
            var typeCommand = typeof(TCommand);

            
        }

        public async Task Start()
        {
            try
            {
                await m_discordBotModel.DiscordClient.LoginAsync(Discord.TokenType.Bot, m_discordBotModel.Token);
                await m_discordBotModel.DiscordClient.StartAsync();
                Log.CoreLogger?.Logging("Discord bot is connected!!", LogLevel.Info);
                await Task.Delay(-1);
            }
            catch (Exception ex)
            {
                Log.CoreLogger?.Logging($"Error start discord bot. Exception when start: {ex}", LogLevel.Error);
            }

       
    }
}
