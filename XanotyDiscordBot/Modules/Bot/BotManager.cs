using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;
using XanotyDiscordBot.Modules.Commands;
using XanotyDiscordBot.Modules.Secret;

namespace XanotyDiscordBot.Modules.Bot
{
    public class BotManager
    {
        public static DiscordSocketClient BotClient;
        public static CommandService Commands;
        public static IServiceProvider ServiceProvider;

        public async Task RunBot()
        {
            BotClient = new DiscordSocketClient();
            Commands = new CommandService();
            ServiceProvider = ConfigureServices();

            await BotClient.LoginAsync(Discord.TokenType.Bot, Config.GetToken());
            await BotClient.StartAsync();

            BotClient.Log += BotLogging;
            BotClient.Ready += BotReady;

            await Task.Delay(-1);
        }

        public Task BotLogging(LogMessage message)
        {
            Console.WriteLine($"Bot: {message}");

            return Task.CompletedTask;
        }

        public async Task BotReady()
        {
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), ServiceProvider);

            await BotClient.SetGameAsync("VRChat");
            BotClient.MessageReceived += BotMessage;
        }

        public async Task BotMessage(SocketMessage arg)
        {
            SocketUserMessage message = arg as SocketUserMessage;

            int commandPosition = 0;

            if (message.HasStringPrefix(Config.prefix, ref commandPosition))
            {
                SocketCommandContext context = new SocketCommandContext(BotClient, message);
                IResult result = await Commands.ExecuteAsync(context, commandPosition, ServiceProvider);

                if (!result.IsSuccess)
                {
                    Console.WriteLine($"Error: {result.ErrorReason}");
                }
            }
        }

        public IServiceProvider ConfigureServices()
        {
            return new ServiceCollection().AddSingleton<CrashCommandModule>().BuildServiceProvider();
        }

    }
}
