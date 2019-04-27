using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsoff.WC.Common.Messengers;
using Vsoff.WC.Common.Modules.Commands.Types;
using Vsoff.WC.Common.Modules.Screenshots;
using Vsoff.WC.Core.Common;

namespace Vsoff.WC.Common.Modules.Commands.Handlers
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
