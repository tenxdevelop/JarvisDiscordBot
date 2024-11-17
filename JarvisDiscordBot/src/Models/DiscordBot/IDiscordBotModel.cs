/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using Discord.Commands;
using Discord.WebSocket;

namespace JarvisDiscordBot.Models
{
    public interface IDiscordBotModel
    {
        DiscordSocketClient DiscordClient { get; }
        CommandService DiscordCommand { get; }
        string Token { get; }
        string Prefix { get; }
    }
}
