using System;
using Vsoff.WC.Common.Messengers;
using Vsoff.WC.Common.Modules.Commands.Types;
using Vsoff.WC.Common.Modules.System.Services;

namespace Vsoff.WC.Common.Modules.Commands.Handlers
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
