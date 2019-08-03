﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsoff.WC.Client.Messengers;
using Vsoff.WC.Client.Modules.Commands.Types;
using Vsoff.WC.Client.Modules.System;
using Vsoff.WC.Client.Modules.System.Services;
using Vsoff.WC.Core.Modules.Commands;

namespace Vsoff.WC.Client.Modules.Commands.Handlers
{
    public class VolumeCommandHandler : CommandHandler<VolumeCommand>
    {
        private readonly IMessenger _messenger;
        private readonly IVolumeService _volumeService;

        public VolumeCommandHandler(
            IVolumeService volumeService,
            IMessenger messenger)
        {
            _volumeService = volumeService ?? throw new ArgumentNullException(nameof(volumeService));
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        }

        public override void Handle(VolumeCommand command)
        {
            switch (command.Type)
            {
                case VolumeCommand.Command.VolumeUp:
                    _volumeService.VolumeUp();
                    break;
                case VolumeCommand.Command.VolumeDown:
                    _volumeService.VolumeDown();
                    break;
                case VolumeCommand.Command.Mute:
                    _volumeService.Mute();
                    break;
                default:
                    _messenger.Send("Неизвестный тип команды настройки автозапуска");
                    break;
            }
        }
    }
}
