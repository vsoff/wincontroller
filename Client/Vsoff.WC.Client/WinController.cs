using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vsoff.WC.Client.Messengers;
using Vsoff.WC.Client.Modules.Commands;
using Vsoff.WC.Client.Modules.System;
using Vsoff.WC.Client.Modules.System.Services;

namespace Vsoff.WC.Client
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

        private readonly IUserMonitoringService _userMonitoringService;
        private readonly ISystemService _systemService;
        private readonly ICommandReceiver _reciever;
        private readonly IMessenger _messenger;

        public WinController(
            IUserMonitoringService userMonitoringService,
            ISystemService systemService,
            ICommandReceiver reciever,
            IMessenger messenger)
        {
            _userMonitoringService = userMonitoringService ?? throw new ArgumentNullException(nameof(userMonitoringService));
            _systemService = systemService ?? throw new ArgumentNullException(nameof(systemService));
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            _reciever = reciever ?? throw new ArgumentNullException(nameof(reciever));

            _manualResetEvent = new ManualResetEvent(true);
        }

        public void Start()
        {
            _messenger.Send($"Машина `{_systemService.MachineName}` запущена\n{DateTime.Now}");

            _userMonitoringService.StartMonitoring();
            _reciever.Start();

            _manualResetEvent.Reset();
        }

        public void Stop()
        {
            _reciever.Stop();
            _userMonitoringService.StopMonitoring();

            _messenger.Send($"Машина `{_systemService.MachineName}` выключена\n{DateTime.Now}");
            _manualResetEvent.Set();
        }

        public void WaitExit()
        {
            _manualResetEvent.WaitOne();
        }
    }
}
