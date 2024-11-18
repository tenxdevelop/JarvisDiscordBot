/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using JarvisDiscordBot.Controller;
using JarvisDiscordBot.ViewModels;

namespace JarvisDiscordBot.Core
{
    public static class DiscordCommandRegistration
    {
        public static void RegisterCommand(IDiscordBotViewModel discordBot)
        {
            discordBot.RegisterCommand<MusicCommand>();
        }
    }
}
