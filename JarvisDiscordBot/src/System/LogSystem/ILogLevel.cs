
using System;

namespace JarvisDiscordBot
{
    public interface ILogLevel : IComparable
    {
        int Level { get; }
        string Name { get; }
        int CompareTo(ILogLevel logLevel);       
    }
}