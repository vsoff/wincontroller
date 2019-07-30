using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Vsoff.WC.Server.Api.Auth;
using Vsoff.WC.Server.Services;

namespace Vsoff.WC.Server
{
    public class ServerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FakeUserService>().As<IUserService>().SingleInstance();
            builder.RegisterType<ServerAuthorizationService>().As<IServerAuthorizationService>().SingleInstance();
        }
    }
}