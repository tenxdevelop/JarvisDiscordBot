/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System;

namespace JarvisDiscordBot
{

    public interface ILogSystem : IDisposable
    {
        string Name { get; }
        void Logging(string message, LogLevel level);
        void AddLogger(ILogger logger);
    }

}