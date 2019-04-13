using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsoff.WC.Common.Messengers;
using Vsoff.WC.Common.Modules.Screenshots;
using Vsoff.WC.Common.Modules.SystemMonitors;
using Vsoff.WC.Core.Common;

namespace Vsoff.WC.Common.Modules.Commands
{
    public interface ICommandService
    {
        void InvokeCommand(CommandType commandType, string argument);
    }

    public class CommandService : ICommandService
    {
        private readonly IScreenshotService _screenshotService;
        private readonly ISystemMonitor _systemController;
        private readonly IMessenger _messenger;

        public CommandService(
            IScreenshotService screenshotService,
            ISystemMonitor systemController,
            IMessenger messenger)
        {
            _screenshotService = screenshotService ?? throw new ArgumentNullException(nameof(screenshotService));
            _systemController = systemController ?? throw new ArgumentNullException(nameof(systemController));
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        }

        public void InvokeCommand(CommandType commandType, string argument)
        {
            switch (commandType)
            {
                case CommandType.Screenshot:
                    _messenger.Send(new NotifyMessage { Photo = _screenshotService.GetScreenshot() });
                    break;
                case CommandType.Status:
                    _messenger.Send(_systemController.GetSystemInfo().ToString());
                    break;

                case CommandType.Unknown:
                default:
                    _messenger.Send("Неизвестная команда");
                    break;
            }
        }

        public void TakeScreenshot() => _messenger.Send(new NotifyMessage { Photo = _screenshotService.GetScreenshot() });
    }
}
