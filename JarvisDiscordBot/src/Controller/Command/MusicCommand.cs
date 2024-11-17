/**************************************************************************\
    Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using JarvisDiscordBot.Models;
using System.Threading.Tasks;

namespace JarvisDiscordBot.Controller
{
    public sealed class MusicCommand : BaseCommandModule, IDiscordCommandModel
    {
        [Command("play")]
        public async Task PlayMusic(CommandContext commandContext, string query)
        {
            var userVoiceChanel = commandContext.Member.VoiceState.Channel;

            if (commandContext.Member.VoiceState is null || userVoiceChanel is null)
            {
                await commandContext.Channel.SendMessageAsync("Please enter a any chanel");
                return;
            }

        }
    }
}
