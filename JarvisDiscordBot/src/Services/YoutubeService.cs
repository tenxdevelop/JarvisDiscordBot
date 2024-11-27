/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using JarvisDiscordBot.Models;
using Newtonsoft.Json.Linq;

namespace JarvisDiscordBot.Services
{
    public class YoutubeService : IYoutubeService
    {
        private string m_apiKey;
        private readonly HttpClient m_http;
        public YoutubeService(Config config) 
        {
            m_apiKey = config.YoutubeApiKey;
            m_http = new HttpClient();
        }

        public async Task<List<string>> GetPlaylistSongs(string url)
        {
            var playlistId = ExtractPlaylistId(url);
            var titles = new List<string>();
            string nextPageToken = "";

            do
            {               
                string urlRequest = $"https://www.googleapis.com/youtube/v3/playlistItems?part=snippet&maxResults=70&playlistId={playlistId}&key={m_apiKey}&pageToken={nextPageToken}";

                
                var response = await m_http.GetStringAsync(urlRequest);
                var json = JObject.Parse(response);
                nextPageToken = json["nextPageToken"]?.ToString(); 

                foreach (var item in json["items"])
                {
                    titles.Add(item["snippet"]["title"].ToString());
                }
            } while (!string.IsNullOrEmpty(nextPageToken));

            return titles;
        }


        private static string ExtractPlaylistId(string url)
        {
            var uri = new Uri(url);
            var query = uri.Query;
            var queryParameters = System.Web.HttpUtility.ParseQueryString(query);
            return queryParameters["list"];
        }
    }
}
