/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

namespace JarvisDiscordBot.Services
{
    public interface IYoutubeService
    {
        Task<List<string>> GetPlaylistSongs(string url);
    }
}
