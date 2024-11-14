/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using JarvisDiscordBot.Models;
using System.Threading.Tasks;

namespace JarvisDiscordBot.Services
{
    public interface IDeserializer
    {
        Task<Config> ReadConfig();
    }
}
