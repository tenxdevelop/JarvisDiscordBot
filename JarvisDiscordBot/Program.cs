﻿/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using JarvisDiscordBot.Core;
using System.Threading.Tasks;

namespace JarvisDiscordBot
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var entryPoint = new EntryPoint();
            await entryPoint.Start();
        }

    }
}
