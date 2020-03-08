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
            await ctx.Channel.SendMessageAsync("Pong").ConfigureAwait(false);
        }

        [Command("Add")]
        [Description("Adds two numbers together")]
        [RequireRoles(RoleCheckMode.Any, "Moderator", "Owner")]
        public async Task Add(CommandContext ctx, 
            [Description("First digit")]int numberOne, 
            [Description("Second digit")]int numberTwo)
        {
            await ctx.Channel
                .SendMessageAsync((numberOne + numberTwo).ToString())
                .ConfigureAwait(false);
        }

        [Command("response")]
        public async Task Response(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();
            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel)
                .ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync(message.Result.Content);
        }
    }
}
