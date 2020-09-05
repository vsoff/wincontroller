using System;
using System.Linq;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Vsoff.WC.Common.Messengers;
using Vsoff.WC.Common.Modules.Commands;
using Vsoff.WC.Common.Modules.Commands.Converters;
using Vsoff.WC.Common.Modules.Commands.Handlers;
using Vsoff.WC.Common.Modules.Config;
using Vsoff.WC.Common.Modules.Screenshots;
using Vsoff.WC.Common.Modules.System.Services;
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
            // Получаем все CommandHandler'ы с помощью рефлексии и регистрируем их.
            var commandHandlerTypes = typeof(CommandHandler<>).Assembly.DefinedTypes
                .Where(x => IsAssignableToGenericType(x, (typeof(CommandHandler<>))))
                .ToArray();

            foreach (var commandHandlerType in commandHandlerTypes)
            {
                if (commandHandlerType.IsAbstract)
                    continue;

                var type = commandHandlerType.UnderlyingSystemType;
                var name = type.Name;
                container.RegisterType(typeof(ICommandHandler), type, name, new SingletonLifetimeManager());
            }
        }

        public static bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
                return true;

            if (givenType.GetInterfaces().Any(it => it.IsGenericType && it.GetGenericTypeDefinition() == genericType))
                return true;

            return givenType.BaseType != null && IsAssignableToGenericType(givenType.BaseType, genericType);
        }
    }
}