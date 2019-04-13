using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using Unity.Lifetime;
using Vsoff.WC.Common.Messengers;
using Vsoff.WC.Common.Modules.Commands;
using Vsoff.WC.Common.Modules.Screenshots;
using Vsoff.WC.Common.Modules.SystemMonitors;
using Vsoff.WC.Common.Notifiers;
using Vsoff.WC.Core.Common.Workers;
using Vsoff.WC.Core.Notifiers;

namespace Vsoff.WC.Common
{
    public static class WinControllerModule
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterType<INotifier, TelegramNotifier>(new SingletonLifetimeManager());
            container.RegisterType<IReceiver, CommandReceiver>(new SingletonLifetimeManager());
            container.RegisterType<IMessenger, Messenger>(new SingletonLifetimeManager());

            container.RegisterType<IWorkerController, DefaultWorkerController>(new SingletonLifetimeManager());
            container.RegisterType<IWinController, WinController>(new SingletonLifetimeManager());

            container.RegisterType<IScreenshotService, ScreenshotService>(new SingletonLifetimeManager());
            container.RegisterType<ICommandService, CommandService>(new SingletonLifetimeManager());

            container.RegisterType<ISystemMonitor, SystemController>(new SingletonLifetimeManager());
        }
    }
}
