/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

namespace JarvisDiscordBot.Models
{
    public class Config
    {
        public string Token { get; set; }
        public string Prefix { get; set; }
        public string LavalinkHostName { get; set; }
        public string LavalinkPort { get; set; }
        public string LavalinkAutorisation { get; set; }
        public string VkToken { get; private set; }
    }
}
