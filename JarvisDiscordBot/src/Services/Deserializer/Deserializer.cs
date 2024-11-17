/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using JarvisDiscordBot.Models;
using System.Threading.Tasks;

namespace JarvisDiscordBot.Services.Deserializer
{
    public class Deserializer : IDeserializer
    {
        private const string JSON_DESERIALIER = nameof(JSONDeserializer);
        private const string CONFIG_FILEPATH = "config.json";

        private IDeserializer m_deserializer;
        public void Init<T>() where T : IDeserializer
        {
            m_deserializer = CreateDeserialier<T>();

        }

        public async Task<Config> ReadConfig()
        {
            return await m_deserializer.ReadConfig();
        }

        private IDeserializer CreateDeserialier<T>() where T : IDeserializer
        {
            var type = typeof(T);
            switch (type.Name)
            {
                case JSON_DESERIALIER:
                    return new JSONDeserializer(CONFIG_FILEPATH);
                default:
                    Log.CoreLogger?.Logging($"Can't Create Deserializer. can't found {type.Name}", LogLevel.Error);
                    return null;

            }
        }
    }
}
