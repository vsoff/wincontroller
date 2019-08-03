using System;
using System.IO;
using System.Net;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;
using Vsoff.WC.Core.Modules.Configs;
using Vsoff.WC.Server.Modules.Configs;
using Vsoff.WC.Server.Modules.Menu;

namespace Vsoff.WC.Server.Modules.Messengers
{
    public class TelegramNotifier : INotifier
    {
        private readonly IConfigService<ServerConfig> _appConfigService;
        private readonly ITelegramMenuBuilder _telegramMenuBuilder;
        private readonly TelegramBotClient _client;

        public TelegramNotifier(
            IConfigService<ServerConfig> appConfigService,
            ITelegramMenuBuilder telegramMenuBuilder)
        {
            _appConfigService = appConfigService ?? throw new ArgumentNullException(nameof(appConfigService));
            _telegramMenuBuilder = telegramMenuBuilder ?? throw new ArgumentNullException(nameof(telegramMenuBuilder));

            var config = _appConfigService.GetConfig();
            IWebProxy proxy = new WebProxy(config.ProxyIp);
            _client = new TelegramBotClient(config.TelegramToken, proxy);
        }

        public void Notify(NotifyMessage msg)
        {
            var adminId = _appConfigService.GetConfig().AdminTelegramId;
            switch (msg.Type)
            {
                case NotifyMessage.DataType.Text:
                    _client.SendTextMessageAsync(adminId, msg.Text).Wait();
                    break;
                case NotifyMessage.DataType.Photo:
                    _client.SendDocumentAsync(adminId,
                        new InputOnlineFile(new MemoryStream(msg.Photo), "screenshot.jpeg"));
                    break;
                case NotifyMessage.DataType.Menu:
                    var markup = _telegramMenuBuilder.BuildMenuMarkup(msg.MenuType);
                    _client.SendTextMessageAsync(adminId, msg.Text, replyMarkup: markup).Wait();
                    break;
            }
        }
    }
}