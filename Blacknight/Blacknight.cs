using DSharpPlus;
using DSharpPlus.CommandsNext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Blacknight
{
    class Blacknight
    {
        private static DiscordClient _client;
        private static BotConfig _config;
        private static IServiceProvider _services;

        public static async Task RunBotAsync()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            _config = await BotConfig.LoadConfigAsync("../../../botconfig.json");

            _client = new DiscordClient(new DiscordConfiguration
            {
                AutoReconnect = true,
                Token = _config.Token,
                MinimumLogLevel = LogLevel.Information,
                LoggerFactory = new LoggerFactory().AddSerilog(),
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged
            });

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_config)
                .BuildServiceProvider();
            
            _client.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { _config.Prefix },
                Services = _services
            }).RegisterCommands(Assembly.GetExecutingAssembly());

            await _client.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
