/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using DSharpPlus.CommandsNext;
using DSharpPlus;

namespace JarvisDiscordBot.Models
{
    public interface IDiscordBotModel
    {
        DiscordClient m_discordClient { get; }
        CommandsNextExtension m_discordCommand { get; }
    }
}
