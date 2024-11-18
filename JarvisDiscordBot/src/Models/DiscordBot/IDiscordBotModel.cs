/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using DSharpPlus.CommandsNext;
using DSharpPlus.Lavalink;
using DSharpPlus;

namespace JarvisDiscordBot.Models
{
    public interface IDiscordBotModel
    {
        DiscordClient DiscordClient { get; }
        CommandsNextExtension DiscordCommand { get; }
        LavalinkConfiguration LavaLinkConfiguration { get; } 
        LavalinkExtension LavaLink { get; }
    }
}
