/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using JarvisDiscordBot.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;

namespace JarvisDiscordBot.Services
{
    public class JSONDeserializer : IDeserializer
    {
        private string m_configPath;

        public JSONDeserializer(string configPath)
        {
            m_configPath = configPath;
        }

        public async Task<Config> ReadConfig()
        {
            string jsonFile = await FileSystem.ReadFromFileAsync(m_configPath);
            try
            {
                return JsonConvert.DeserializeObject<Config>(jsonFile);
            }
            catch(Exception ex)
            {
                Log.CoreLogger?.Logging($"Error deserialize config. Exception: {ex}", LogLevel.Error);
                return null;
            }
        }

    }
}
