using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Threading.Tasks;
using Serilog;

namespace Blacknight.Modules
{
    public class TestModule : BaseCommandModule
    {
        [Command("ping")]
        public async Task PingCommand(CommandContext ctx)
        {
            var embed = new DiscordEmbedBuilder()
               .WithColor(DiscordColor.Aquamarine)
               .WithTitle("Pong! :ping_pong:")
               .AddField("WS Latency", $"{ctx.Client.Ping}ms")
               .WithTimestamp(DateTime.UtcNow);

            await ctx.RespondAsync(embed.Build());
            Log.Information($"{ctx.Message.Author.Username} ran the ping command!");
        }

        [Command("echo")]
        public async Task EchoCommand(CommandContext ctx, [RemainingText] string msg)
        {
            await ctx.RespondAsync(msg.Replace("@", ""));
            Log.Information($"{ctx.Message.Author.Username} ran the echo command!");
        } 
    }
}
