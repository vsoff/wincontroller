﻿using System;
using Vsoff.WC.Common.Messengers;
using Vsoff.WC.Common.Modules.Commands.Types;
using Vsoff.WC.Common.Modules.System.Services;

namespace Vsoff.WC.Common.Modules.Commands.Handlers
{
    public class ShutdownCommandHandler : CommandHandler<ShutdownCommand>
    {
        private readonly IMessenger _messenger;
        private readonly ISystemService _systemService;

        public ShutdownCommandHandler(
            ISystemService systemService,
            IMessenger messenger)
        {
            _systemService = systemService ?? throw new ArgumentNullException(nameof(systemService));
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        }

        public override void Handle(ShutdownCommand command)
        {
            if (command.IsAbort)
            {
                _systemService.ShutdownAbort();
                _messenger.Send($"Выключение компьютера было приостановлено");
            }
            else
            {
                _systemService.Shutdown(command.Delay);
                _messenger.Send($"Компьютер будет выключен через {command.Delay}");
            }
        }
    }
}
