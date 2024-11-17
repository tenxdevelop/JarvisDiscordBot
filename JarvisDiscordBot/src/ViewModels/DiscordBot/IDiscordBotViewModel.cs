/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using JarvisDiscordBot.Models;
using System.Threading.Tasks;

namespace JarvisDiscordBot.ViewModels
{
    public interface IDiscordBotViewModel
    {
        void RegisterCommand<TCommand>() where TCommand : IDiscordCommandModel;

        Task Start();
    }
}
