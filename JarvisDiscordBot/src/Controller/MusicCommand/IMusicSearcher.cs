/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using DSharpPlus.Lavalink;
using System.Collections.Generic;

namespace JarvisDiscordBot.Controller
{
    internal interface IMusicSearcher
    {
        IAsyncEnumerable<LavalinkTrack> SearchMusic(LavalinkNodeConnection node, string query);
    }
}
