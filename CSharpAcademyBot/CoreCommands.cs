using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAcademyBot
{
    internal class CoreCommands : BaseCommandModule
    {
        [Command("test")]
        public async Task Test(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("test successful.");
        }
    }
}
