using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class BasicCommands : BaseCommandModule
    {
        [Command("ping")]
        [Description("Returns 'pong'")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("pong").ConfigureAwait(false);
        }

        [Command("add")]
        [Description("Adds two numbers together")]
        public async Task Add(CommandContext ctx, 
            [Description("First digit")]int numberOne, 
            [Description("Second digit")]int numberTwo)
        {
            await ctx.Channel
                .SendMessageAsync((numberOne + numberTwo).ToString())
                .ConfigureAwait(false);
        }

        [Command("respondmessage")]
        public async Task RespondMessage(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();
            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel)
                .ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync(message.Result.Content);
        }

        [Command("respondreaction")]
        public async Task RespondReaction(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();
            var message = await interactivity.WaitForReactionAsync(x => x.Channel == ctx.Channel);
            await ctx.Channel.SendMessageAsync(message.Result.Emoji);
        }
    }
}
