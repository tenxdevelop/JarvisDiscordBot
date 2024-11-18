/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/


using Microsoft.Extensions.Logging;
using Discord.Rest;

namespace JarvisDiscordBot
{
    public class DiscordLogger : ILogger<BaseDiscordClient>
    {
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            return logLevel >= Microsoft.Extensions.Logging.LogLevel.Debug;
        }

        public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            var logMessage = formatter(state, exception);

            if (!string.IsNullOrEmpty(logMessage))
            {
                var myLogLevel = LogLevel.GetLogLevelFromLevel(Microsoft.Extensions.Logging.LogLevel.None.CompareTo(logLevel)) as LogLevel ?? LogLevel.Defualt;
                JarvisDiscordBot.Log.CoreLogger?.Logging(logMessage, myLogLevel);
            }
        }
    }
}
