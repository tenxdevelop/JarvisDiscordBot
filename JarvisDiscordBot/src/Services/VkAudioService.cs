using JarvisDiscordBot.Models;
using Newtonsoft.Json.Linq;

namespace JarvisDiscordBot.Services
{
    public class VkAudioService
    {
        private readonly HttpClient httpClient;
        private readonly string accessToken;

        public VkAudioService(Config config)
        {
            accessToken = config.VkToken;
            httpClient = new HttpClient();
        }

        public async Task<string> FindAudioUrlByNameAsync(string query)
        {
            var encodedQuery = Uri.EscapeDataString(query);
            var url = $"https://api.vk.com/method/audio.search?q={encodedQuery}&v=5.131&access_token={accessToken}&count=1";

            var response = await httpClient.GetStringAsync(url);
            var json = JObject.Parse(response);

            
            if (json["response"]?["items"]?.Count() > 0)
            {
                
                var audioUrl = json["response"]["items"][0]["url"].ToString();
                return audioUrl;
            }
            else
            {
                Log.CoreLogger?.Logging("Can't find audio url form vk api", LogLevel.Error);
                return string.Empty;
            }
        }
    }
}
