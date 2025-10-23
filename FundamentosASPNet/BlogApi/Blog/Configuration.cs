namespace Blog
{
    public static class Configuration
    {
        //TOKEN - JWT = Json Web Token
        public static string JwtKey { get; set; } = "ZmVKKTBNCJEDNEJDENJWNjdneunduUNUN"; //Pode ser um GUID
        public static string ApiKeyName = "api_kwy";
        public static string ApiKey = "Curso_api_IlTevUM/z0ey3NwCV/unWg==*";
        public static SmtpConfiguration smtp = new();

        public class SmtpConfiguration
        {
            //dados necessario para envios por Email
            public string Host { get; set; }
            public int Port { get; set; } = 25;
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}
