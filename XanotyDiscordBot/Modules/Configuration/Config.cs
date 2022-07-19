namespace XanotyDiscordBot.Modules.Secret
{
    public class Config
    {
        private static string token = "Token_here";
        public const string prefix = "!";

        public static string GetToken()
        {
            return token;
        }
    }
}
