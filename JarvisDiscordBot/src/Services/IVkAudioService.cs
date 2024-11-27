/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

namespace JarvisDiscordBot.Services
{
    public interface IVkAudioService
    {
        Task<string> FindAudioUrlByNameAsync(string query);
    }
}
