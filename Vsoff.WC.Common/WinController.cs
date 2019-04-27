using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
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
        void WaitExit();
        void Stop();
    }

    public class WinController : IWinController
    {
        private readonly ManualResetEvent _manualResetEvent;

        private readonly ISystemService _systemService;
        private readonly ICommandReceiver _reciever;
        private readonly IMessenger _messenger;

        public WinController(
            ISystemService systemService,
            ICommandReceiver reciever,
            IMessenger messenger)
        {
            _systemService = systemService ?? throw new ArgumentNullException(nameof(systemService));
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            _reciever = reciever ?? throw new ArgumentNullException(nameof(reciever));

            _manualResetEvent = new ManualResetEvent(true);
        }

        public void Start()
        {
            _messenger.Send($"Машина `{_systemService.MachineName}` запущена\n{DateTime.Now}");
            _reciever.Start();
            _manualResetEvent.Reset();
        }

        public void Stop()
        {
            _reciever.Stop();
            _messenger.Send($"Машина `{_systemService.MachineName}` выключена\n{DateTime.Now}");
            _manualResetEvent.Set();
        }

        public void WaitExit()
        {
            _manualResetEvent.WaitOne();
        }
    }
}
