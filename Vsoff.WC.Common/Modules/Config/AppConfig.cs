using System;

namespace Vsoff.WC.Common.Modules.Config
{
    public class AppConfig : ICloneable
    {
        public long AdminId { get; set; }
        public string TelegramToken { get; set; }
        public TimeSpan UserActivitySessionDuration { get; set; }

        public AppConfig()
        {
            AdminId = -1;
            TelegramToken = "000000:AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
            UserActivitySessionDuration = TimeSpan.FromMinutes(5);
        }

        public AppConfig GetClone() => (AppConfig)MemberwiseClone();
        public object Clone() => MemberwiseClone();
    }
}
