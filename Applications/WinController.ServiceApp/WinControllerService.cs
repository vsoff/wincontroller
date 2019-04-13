using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Vsoff.WC.Common;
using Vsoff.WC.Common.Messengers;

namespace WinController.ServiceApp
{
    partial class WinControllerService : ServiceBase
    {
        private readonly IMessenger _messenger;
        private readonly IWinController _controller;
        private readonly IUnityContainer _container;

        public WinControllerService()
        {
            InitializeComponent();

            var container = new UnityContainer();
            WinControllerModule.Register(container);

            _container = container;
            _controller = _container.Resolve<IWinController>();
            _messenger = _container.Resolve<IMessenger>();
        }

        protected override void OnShutdown()
        {
            _messenger.Send($"[OnShutdown]: {DateTime.Now}");
            base.OnShutdown();
        }

        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            _messenger.Send($"[OnPowerEvent]: PowerStatus: {powerStatus} at {DateTime.Now}");
            return base.OnPowerEvent(powerStatus);
        }

        protected override void OnStart(string[] args)
        {
            _controller.Start();
        }

        protected override void OnStop()
        {
            _controller.Stop();
        }
    }
}
