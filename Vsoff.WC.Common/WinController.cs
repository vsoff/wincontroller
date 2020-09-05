using System;
using System.Threading;
using Vsoff.WC.Common.Messengers;
using Vsoff.WC.Common.Modules.Commands;
using Vsoff.WC.Common.Modules.System.Services;

namespace Vsoff.WC.Common
{
    public class WinController : IWinController
    {
        private readonly ManualResetEvent _manualResetEvent;

        private readonly IUserMonitoringService _userMonitoringService;
        private readonly ISystemService _systemService;
        private readonly ICommandReceiver _receiver;
        private readonly IMessenger _messenger;

        public WinController(
            IUserMonitoringService userMonitoringService,
            ISystemService systemService,
            ICommandReceiver receiver,
            IMessenger messenger)
        {
            _userMonitoringService = userMonitoringService ?? throw new ArgumentNullException(nameof(userMonitoringService));
            _systemService = systemService ?? throw new ArgumentNullException(nameof(systemService));
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            _receiver = receiver ?? throw new ArgumentNullException(nameof(receiver));

            _manualResetEvent = new ManualResetEvent(true);
        }

        public void Start()
        {
            _messenger.Send($"Машина `{_systemService.MachineName}` запущена\n{DateTime.Now}");

            _userMonitoringService.StartMonitoring();
            _receiver.Start();

            _manualResetEvent.Reset();
        }

        public void Stop()
        {
            _receiver.Stop();
            _userMonitoringService.StopMonitoring();

            _messenger.Send($"Машина `{_systemService.MachineName}` выключена\n{DateTime.Now}");
            _manualResetEvent.Set();
        }

        public void WaitExit() => _manualResetEvent.WaitOne();
    }
}