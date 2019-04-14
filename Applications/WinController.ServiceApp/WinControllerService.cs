using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Vsoff.WC.Common;
using Vsoff.WC.Common.Messengers;
using Vsoff.WC.Common.Modules.System;
using Vsoff.WC.Core.Common;
using Vsoff.WC.Core.Notifiers;

namespace WinController.ServiceApp
{
    partial class WinControllerService : ServiceBase
    {
        private INotifier _notifier;
        private IWinController _controller;
        private IUnityContainer _container;

        public WinControllerService()
        {
            InitializeComponent();

            var container = new UnityContainer();
            WinControllerModule.Register(container);

            _container = container;
            _controller = _container.Resolve<IWinController>();
            _notifier = _container.Resolve<INotifier>();
        }

        protected override void OnShutdown()
        {
            _notifier?.Notify(new NotifyMessage
            {
                Text = $"[OnShutdown]: {DateTime.Now}"
            });

            base.OnShutdown();
        }

        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            _notifier?.Notify(new NotifyMessage
            {
                Text = $"[OnPowerEvent]: PowerStatus: {powerStatus} at {DateTime.Now}"
            });

            return base.OnPowerEvent(powerStatus);
        }

        protected override void OnStart(string[] args)
        {
            _controller.Start();
        }

        protected override void OnStop()
        {
            _controller?.Stop();
        }
    }
}
