using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;
using Vsoff.WC.Client.Modules.Config;

namespace Vsoff.WC.Client.Notifiers
{
    public class TelegramNotifier : INotifier
    {
        private readonly IAppConfigService _appConfigService;
        private readonly TelegramBotClient _client;

        public TelegramNotifier(IAppConfigService appConfigService)
        {
            _appConfigService = appConfigService ?? throw new ArgumentNullException(nameof(appConfigService));

            var config = _appConfigService.GetConfig();
            IWebProxy proxy = new WebProxy(config.ProxyIp);
            _client = new TelegramBotClient(config.TelegramToken, proxy);
        }

        public void Notify(NotifyMessage msg)
        {
            var adminId = _appConfigService.GetConfig().AdminId;
            if (msg.Photo != null && msg.Photo.Length > 0)
                _client.SendDocumentAsync(adminId, new InputOnlineFile(new MemoryStream(msg.Photo), "screenshot.jpeg"));
            else
                _client.SendTextMessageAsync(adminId, msg.Text).Wait();
        }
    }
}
