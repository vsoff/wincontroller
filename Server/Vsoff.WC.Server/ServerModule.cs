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
            builder.RegisterType<DefaultWorkerController>().As<IWorkerController>().SingleInstance();
            builder.RegisterType<TelegramMenuBuilder>().As<ITelegramMenuBuilder>().SingleInstance();
            builder.RegisterType<Messenger>().As<IMessenger>().SingleInstance().AutoActivate();
            builder.RegisterType<CommandConverter>().As<ICommandConverter>().SingleInstance();
            builder.RegisterType<CommandInvoker>().As<ICommandInvoker>().SingleInstance();
            builder.RegisterType<FakeUserService>().As<IUserService>().SingleInstance();
            builder.RegisterType<FakeCommandService>().As<ICommandService>().SingleInstance();

            if (config.DebugMode)
            {
                builder.RegisterType<ConsoleCommandReceiver>().As<ICommandReceiver>().SingleInstance().AutoActivate();
                builder.RegisterType<ConsoleNotifier>().As<INotifier>().SingleInstance();
            }
            else
            {
                builder.RegisterType<TelegramCommandReceiver>().As<ICommandReceiver>().SingleInstance().AutoActivate();
                builder.RegisterType<TelegramNotifier>().As<INotifier>().SingleInstance();
            }

            RegisterCommandHandlers(builder);
        }

        private void RegisterCommandHandlers(ContainerBuilder builder)
        {
            builder.RegisterType<UndefinedCommandHandler>().As<ICommandHandler>().SingleInstance();
            builder.RegisterType<MenuCommandHandler>().As<ICommandHandler>().SingleInstance();

            builder.RegisterType<RemoteCommandHandler<StatusCommand>>().As<ICommandHandler>().SingleInstance();
        }
    }
}