using DSharpPlus;
using DSharpPlus.CommandsNext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Emzi0767.Utilities;

namespace TemplateBot
{
    public static class TemplateBot
    {
        private static DiscordClient _client;
        private static BotConfig _config;
        private static IServiceProvider _services;

        public static async Task<int> RunBotAsync()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Async(cf =>
                {
                    cf.Console();
                })
                .CreateLogger();
            
            Directory.SetCurrentDirectory(Directory.GetCurrentDirectory());
            _config = await BotConfig.LoadConfigAsync("botconfig.json");
            
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
                StringPrefixes = new[] { _config.Prefix },
                Services = _services
            }).RegisterCommands(Assembly.GetExecutingAssembly());

            try
            {
                await _client.ConnectAsync();
                await Task.Delay(-1);
                return 0;
            }
            catch (Exception exception)
            {
                Log.Fatal(exception, "Host terminated unexpectedly, shutting down.");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
