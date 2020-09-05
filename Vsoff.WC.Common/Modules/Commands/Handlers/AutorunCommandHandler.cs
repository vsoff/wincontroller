using System;
using Vsoff.WC.Common.Messengers;
using Vsoff.WC.Common.Modules.Commands.Types;
using Vsoff.WC.Common.Modules.System.Services;

namespace Vsoff.WC.Common.Modules.Commands.Handlers
{
    public class AutorunCommandHandler : CommandHandler<AutorunCommand>
    {
        private readonly System.Services.IAutorunService _autorunService;
        private readonly IMessenger _messenger;

        public AutorunCommandHandler(
            IAutorunService autorunService,
            IMessenger messenger)
        {
            _autorunService = autorunService ?? throw new ArgumentNullException(nameof(autorunService));
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        }

        public override void Handle(AutorunCommand command)
        {
            switch (command.Type)
            {
                case AutorunCommand.Command.AddAutorun:
                    if (_autorunService.IsRegisterExists())
                    {
                        _messenger.Send("[НЕУДАЧА] приложение уже зарегистрировано в автозапуске");
                    }
                    else
                    {
                        _autorunService.Register();
                        _messenger.Send("[УСПЕХ] приложение зарегистрировано в автозапуске");
                    }
                    break;
                case AutorunCommand.Command.RemoveAutorun:
                    if (!_autorunService.IsRegisterExists())
                    {
                        _messenger.Send("[НЕУДАЧА] приложение ещё не зарегистрировано в автозапуске");
                    }
                    else
                    {
                        _autorunService.Unregister();
                        _messenger.Send("[УСПЕХ] приложение удалено из автозапуске");
                    }
                    break;
                default:
                    _messenger.Send("Неизвестный тип команды настройки автозапуска");
                    break;
            }
        }
    }
}
