using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using Unity.Lifetime;
using Vsoff.WC.Client.Messengers;
using Vsoff.WC.Client.Modules.Commands;
using Vsoff.WC.Client.Modules.Commands.Converters;
using Vsoff.WC.Client.Modules.Commands.Handlers;
using Vsoff.WC.Client.Modules.Config;
using Vsoff.WC.Client.Modules.Screenshots;
using Vsoff.WC.Client.Modules.System;
using Vsoff.WC.Client.Modules.System.Services;
using Vsoff.WC.Client.Notifiers;
using Vsoff.WC.Common.Workers;

namespace Vsoff.WC.Client
{
    public static class WinControllerModule
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterType<INotifier, TelegramNotifier>(new SingletonLifetimeManager());
            container.RegisterType<ICommandReceiver, CommandReceiver>(new SingletonLifetimeManager());

            RegisterCommon(container);
        }

        public static void RegisterTesting(IUnityContainer container)
        {
            container.RegisterType<INotifier, ConsoleNotifier>(new SingletonLifetimeManager());
            container.RegisterType<ICommandReceiver, ConsoleCommandReceiver>(new SingletonLifetimeManager());

            RegisterCommon(container);
        }

        private static void RegisterCommon(IUnityContainer container)
        {
            container.RegisterType<ICommandConverter, CommandConverter>(new SingletonLifetimeManager());
            container.RegisterType<IMessenger, Messenger>(new SingletonLifetimeManager());

            container.RegisterType<IWorkerController, DefaultWorkerController>(new SingletonLifetimeManager());
            container.RegisterType<IWinController, WinController>(new SingletonLifetimeManager());

            // Сервисы.
            container.RegisterType<IUserMonitoringService, UserMonitoringService>(new SingletonLifetimeManager());
            container.RegisterType<IAutorunService, AutorunScheduleService>(new SingletonLifetimeManager());
            container.RegisterType<IScreenshotService, ScreenshotService>(new SingletonLifetimeManager());
            container.RegisterType<IAppConfigService, AppConfigService>(new SingletonLifetimeManager());
            container.RegisterType<ICommandService, CommandService>(new SingletonLifetimeManager());
            container.RegisterType<ISystemService, SystemService>(new SingletonLifetimeManager());
            container.RegisterType<IVolumeService, VolumeService>(new SingletonLifetimeManager());

            RegisterCommandHandlers(container);
        }

        private static void RegisterCommandHandlers(IUnityContainer container)
        {
            container.RegisterType<ICommandHandler, TakeScreenshotCommandHandler>(nameof(TakeScreenshotCommandHandler), new SingletonLifetimeManager());
            container.RegisterType<ICommandHandler, UndefinedCommandHandler>(nameof(UndefinedCommandHandler), new SingletonLifetimeManager());
            container.RegisterType<ICommandHandler, ShutdownCommandHandler>(nameof(ShutdownCommandHandler), new SingletonLifetimeManager());
            container.RegisterType<ICommandHandler, KeyboardCommandHandler>(nameof(KeyboardCommandHandler), new SingletonLifetimeManager());
            container.RegisterType<ICommandHandler, AutorunCommandHandler>(nameof(AutorunCommandHandler), new SingletonLifetimeManager());
            container.RegisterType<ICommandHandler, StatusCommandHandler>(nameof(StatusCommandHandler), new SingletonLifetimeManager());
            container.RegisterType<ICommandHandler, VolumeCommandHandler>(nameof(VolumeCommandHandler), new SingletonLifetimeManager());
            container.RegisterType<ICommandHandler, LockCommandHandler>(nameof(LockCommandHandler), new SingletonLifetimeManager());
        }
    }
}
