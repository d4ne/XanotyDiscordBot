using System;
using XanotyDiscordBot.Modules.Bot;

namespace XanotyDiscordBot
{
    class Program
    {
        static void Main(string[] args)
        {
            BotManager manager = new BotManager();
            manager.RunBot().GetAwaiter().GetResult();
        }
    }
}
