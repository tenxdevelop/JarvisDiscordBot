/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using DSharpPlus.Lavalink;
using JarvisDiscordBot.Services;

namespace JarvisDiscordBot.Controller
{
    internal class NameMusicVkSearcher : IMusicSearcher
    {
        private IVkAudioService m_vkAudioService;

        public NameMusicVkSearcher(IVkAudioService audioService)
        {
            m_vkAudioService = audioService;
        }

        public async IAsyncEnumerable<LavalinkTrack> SearchMusic(LavalinkNodeConnection node, string query)
        {
            var audioUrl = await m_vkAudioService.FindAudioUrlByNameAsync(query);
            var searchQuery = await node.Rest.GetTracksAsync(audioUrl);
            if (searchQuery.LoadResultType == LavalinkLoadResultType.NoMatches ||
                searchQuery.LoadResultType == LavalinkLoadResultType.LoadFailed)
            {
                yield return null;
            }
            else
            {
                yield return searchQuery.Tracks.First();
            }
           
        }
    }
}
