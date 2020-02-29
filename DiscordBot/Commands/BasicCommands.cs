using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class BasicCommands : BaseCommandModule
    {
        [Command("ping")]
        [Description("Returns 'pong'")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Pong").ConfigureAwait(false);
        }

        //Basic command with arguments
        [Command("Add")]
        [Description("Adds two numbers together")]
        public async Task Add(CommandContext ctx, [Description("First digit")]int numberOne, [Description("Second digit")]int numberTwo)
        {
            await ctx.Channel
                .SendMessageAsync((numberOne + numberTwo).ToString())
                .ConfigureAwait(false);
        }
    }
}
