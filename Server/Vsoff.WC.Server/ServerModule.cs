using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Vsoff.WC.Client.Modules.Commands.Types;
using Vsoff.WC.Common.Workers;
using Vsoff.WC.Core.Modules.Commands;
using Vsoff.WC.Core.Modules.Commands.Handlers;
using Vsoff.WC.Core.Modules.Configs;
using Vsoff.WC.Server.Api.Auth;
using Vsoff.WC.Server.Modules;
using Vsoff.WC.Server.Modules.Commands;
using Vsoff.WC.Server.Modules.Commands.Converters;
using Vsoff.WC.Server.Modules.Commands.Handlers;
using Vsoff.WC.Server.Modules.Configs;
using Vsoff.WC.Server.Modules.Menu;
using Vsoff.WC.Server.Modules.Messengers;
using Vsoff.WC.Server.Services;

namespace Vsoff.WC.Server
{
    public class ServerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var configService = new ServerConfigService();
            var config = configService.GetConfig();

            builder.RegisterInstance(configService).As<IConfigService<ServerConfig>>().SingleInstance();

            builder.RegisterType<ServerAuthorizationService>().As<IServerAuthorizationService>().SingleInstance();
            builder.RegisterType<CommandAutoInvoker>().As<ICommandAutoInvoker>().SingleInstance().AutoActivate();
            builder.RegisterType<TelegramMessenger>().As<IMessenger>().SingleInstance().AutoActivate();
            builder.RegisterType<TelegramMenuProvider>().As<ITelegramMenuProvider>().SingleInstance();
            builder.RegisterType<DefaultWorkerController>().As<IWorkerController>().SingleInstance();
            builder.RegisterType<CommandConverter>().As<ICommandConverter>().SingleInstance();
            builder.RegisterType<CommandInvoker>().As<ICommandInvoker>().SingleInstance();

            builder.RegisterType<FakeAccountContextService>().As<IAccountContextService>().SingleInstance();
            builder.RegisterType<FakeAccountService>().As<IAccountService>().SingleInstance();
            builder.RegisterType<FakeCommandService>().As<ICommandService>().SingleInstance();

            RegisterCommandHandlers(builder);
        }

        private void RegisterCommandHandlers(ContainerBuilder builder)
        {
            builder.RegisterType<UndefinedCommandHandler>().As<ICommandHandler>().SingleInstance();
            builder.RegisterType<MenuCommandHandler>().As<ICommandHandler>().SingleInstance();

            builder.RegisterType<RemoteCommandHandler<AutorunCommand>>().As<ICommandHandler>().SingleInstance();
            builder.RegisterType<RemoteCommandHandler<KeyboardCommand>>().As<ICommandHandler>().SingleInstance();
            builder.RegisterType<RemoteCommandHandler<LockCommand>>().As<ICommandHandler>().SingleInstance();
            builder.RegisterType<RemoteCommandHandler<ShutdownCommand>>().As<ICommandHandler>().SingleInstance();
            builder.RegisterType<RemoteCommandHandler<StatusCommand>>().As<ICommandHandler>().SingleInstance();
            builder.RegisterType<RemoteCommandHandler<TakeScreenshotCommand>>().As<ICommandHandler>().SingleInstance();
            builder.RegisterType<RemoteCommandHandler<VolumeCommand>>().As<ICommandHandler>().SingleInstance();
        }
    }
}