using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsoff.WC.Common.Messengers;
using Vsoff.WC.Common.Modules.Screenshots;
using Vsoff.WC.Common.Modules.System;
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
        private readonly ISystemController _systemController;
        private readonly IMessenger _messenger;

        public CommandService(
            IScreenshotService screenshotService,
            ISystemController systemController,
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
                    try
                    {
                        var photo = _screenshotService.GetScreenshot();
                        _messenger.Send(new NotifyMessage { Photo = _screenshotService.GetScreenshot() });
                    }
                    catch (Exception ex)
                    {
                        _messenger.Send($"Не удалось сделать скриншот.\n\nПричина: {ex}");
                    }
                    break;
                case CommandType.Status:
                    _messenger.Send(_systemController.GetSystemInfo().ToString());
                    break;
                case CommandType.Shutdown:
                    int seconds;
                    bool isSuccess = int.TryParse(argument, out seconds);
                    if (!isSuccess || string.IsNullOrWhiteSpace(argument))
                    {
                        _messenger.Send("Аргумент должен быть целым числом и обозначать кол-во секунд до выключения");
                        return;
                    }
                    _systemController.Shutdown(TimeSpan.FromSeconds(seconds));
                    break;
                case CommandType.ShutdownAbort:
                    _systemController.ShutdownAbort();
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
