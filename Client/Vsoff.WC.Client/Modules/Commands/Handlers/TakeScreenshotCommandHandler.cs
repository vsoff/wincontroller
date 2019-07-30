﻿using System;
using Vsoff.WC.Client.Messengers;
using Vsoff.WC.Client.Modules.Commands.Types;
using Vsoff.WC.Client.Modules.Screenshots;
using Vsoff.WC.Client.Notifiers;

namespace Vsoff.WC.Client.Modules.Commands.Handlers
{
    public class TakeScreenshotCommandHandler : CommandHandler<TakeScreenshotCommand>
    {
        private readonly IMessenger _messenger;
        private readonly IScreenshotService _screenshotService;

        public TakeScreenshotCommandHandler(
            IScreenshotService screenshotService,
            IMessenger messenger)
        {
            _screenshotService = screenshotService ?? throw new ArgumentNullException(nameof(screenshotService));
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        }

        public override void Handle(TakeScreenshotCommand command)
        {
            try
            {
                byte[] screenData = _screenshotService.GetScreenshot();
                _messenger.Send(new NotifyMessage
                {
                    Photo = screenData
                });
            }
            catch (Exception ex)
            {
                _messenger.Send($"Не удалось сделать скриншот.\nОшибка: {ex.Message}");
            }
        }
    }
}