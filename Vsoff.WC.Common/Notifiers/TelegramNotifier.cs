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
using Vsoff.WC.Core.Common;
using Vsoff.WC.Core.Notifiers;

namespace Vsoff.WC.Common.Notifiers
{
    public class TelegramNotifier : INotifier
    {
        private readonly TelegramBotClient _client;

        public TelegramNotifier()
        {
            IWebProxy proxy = new WebProxy(TempConfig.ProxyIp);
            _client = new TelegramBotClient(TempConfig.TelegramToken, proxy);
        }

        public void Notify(NotifyMessage msg)
        {
            if (msg.Photo.Length > 0)
                _client.SendDocumentAsync(TempConfig.AdminId, new InputOnlineFile(new MemoryStream(msg.Photo), "screenshot.jpeg"));
            else
                _client.SendTextMessageAsync(TempConfig.AdminId, msg.Text);
        }
    }
}
