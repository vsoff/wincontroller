using System;
using System.ServiceProcess;
using Unity;
using Vsoff.WC.Common;
using Vsoff.WC.Core.Common;
using Vsoff.WC.Core.Notifiers;

namespace WinController.ServiceApp
{
    partial class WinControllerService : ServiceBase
    {
        private readonly INotifier _notifier;
        private readonly IWinController _controller;
        private readonly IUnityContainer _container;

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
            _controller?.Start();
        }

        protected override void OnStop()
        {
            _controller?.Stop();
        }
    }
}
