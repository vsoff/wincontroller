using Vsoff.WC.Core.Modules.Configs;

namespace Vsoff.WC.Server.Modules.Configs
{
    public class ServerConfig : ConfigBase
    {
        public int AdminTelegramId { get; set; }
        public string ProxyIp { get; set; }
        public string TelegramToken { get; set; }
        public string SecretKey { get; set; }
        public bool DebugMode { get; set; }
    }
}