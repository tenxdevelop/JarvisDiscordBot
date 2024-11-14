/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System;

namespace JarvisDiscordBot
{
    public class ConsoleLogger : BaseLogger<ConsoleLogger>
    {


        public ConsoleLogger()
        {
            
        }

        public override void Dispose()
        {
            
        }

        public override void Logging(string message, LogLevel level)
        {
            Console.ForegroundColor = GetColorFromLevel(level);
            Console.WriteLine(Patern(message, level));
        }

        private ConsoleColor GetColorFromLevel(LogLevel logLevel)
        {
            var writeToColor = ConsoleColor.White;
            if (logLevel.Equals(LogLevel.Defualt))
                writeToColor = ConsoleColor.Blue;
            else if (logLevel.Equals(LogLevel.Info))
                writeToColor = ConsoleColor.Green;
            else if (logLevel.Equals(LogLevel.Warn))
                writeToColor = ConsoleColor.Yellow;
            else if (logLevel.Equals(LogLevel.Error))
                writeToColor = ConsoleColor.Red;
            else if (logLevel.Equals(LogLevel.Crytical))
                writeToColor = ConsoleColor.DarkRed;
            return writeToColor;
        }
    }
}
