﻿using System;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;
using Vsoff.WC.Common.Modules.Config;
using Vsoff.WC.Core.Common;
using Vsoff.WC.Core.Notifiers;

namespace Vsoff.WC.Common.Notifiers
{
    public class TelegramNotifier : INotifier
    {
        private readonly IAppConfigService _appConfigService;
        private readonly TelegramBotClient _client;

        public TelegramNotifier(IAppConfigService appConfigService)
        {
            _appConfigService = appConfigService ?? throw new ArgumentNullException(nameof(appConfigService));

            var config = _appConfigService.GetConfig();
            _client = new TelegramBotClient(config.TelegramToken);
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
