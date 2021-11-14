using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace XanotyDiscordBot.Modules.Commands
{
    public class CrashCommandModule : ModuleBase<SocketCommandContext>
    {
        [Command("c")]
        public async Task CrashWorld(string world)
        {
            try
            {
                var User = Context.User as SocketGuildUser;
                var role = Context.Guild.Roles.FirstOrDefault(x => x.Id == 909107699189432331);

                if (User.Roles.Contains(role))
                {
                    if (Context.Channel.Id.ToString() == "909112409946480640")
                    {
                        string batFile = @"C:\Users\Administrator\Desktop\DiscordBot\run.bat";

                        var process = new Process
                        {
                            StartInfo = {
                                Arguments = $"{world} crash",
                                UseShellExecute = true,
                                CreateNoWindow = false,
                                WindowStyle = ProcessWindowStyle.Normal
                            }
                        };

                        process.StartInfo.FileName = batFile;
                        bool status = process.Start();

                        if (status)
                        {
                            var YourEmoji = new Emoji("👍");
                            await Context.Message.AddReactionAsync(YourEmoji);
                        }
                        else
                        {
                            var YourEmoji = new Emoji("👎");
                            await Context.Message.AddReactionAsync(YourEmoji);
                        }
                    }
                }
                else
                {
                    var YourEmoji = new Emoji("👎");
                    await Context.Message.AddReactionAsync(YourEmoji);
                }          
            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync($"Exception: {ex.Message}");
            }
        }
    }
}
