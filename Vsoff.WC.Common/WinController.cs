using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vsoff.WC.Common.Messengers;
using Vsoff.WC.Common.Modules.Commands;
using Vsoff.WC.Common.Modules.System;
using Vsoff.WC.Core.Common.Workers;
using Vsoff.WC.Core.Notifiers;

namespace Vsoff.WC.Common
{
    public interface IWinController
    {
        void Start();
        void Stop();
    }

    public class WinController : IWinController
    {
        private readonly TimeSpan _statusReportInterval = TimeSpan.FromSeconds(30);

        private readonly ISystemService _systemService;
        private readonly IMessenger _messenger;
        private readonly ICommandReceiver _reciever;

        public WinController(
            IWorkerController workerController,
            ISystemService systemService,
            IMessenger messenger,
            ICommandReceiver reciever)
        {
            _systemService = systemService ?? throw new ArgumentNullException(nameof(systemService));
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            _reciever = reciever ?? throw new ArgumentNullException(nameof(reciever));
        }

        public void Start()
        {
            _messenger.Send($"Машина `{_systemService.MachineName}` запущена\n{DateTime.Now}");
            _reciever.Start();
        }

        public void Stop()
        {
            _reciever.Stop();
            _messenger.Send($"Машина `{_systemService.MachineName}` выключена\n{DateTime.Now}");
        }
    }
}
