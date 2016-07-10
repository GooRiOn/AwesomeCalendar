using System;
using Autofac;
using AwesomeCalendar.Web.Controllers;

namespace AwesomeCalendar.Web.DependencyInjection
{
    public class Registration 
    {
        public static void Register(ContainerBuilder containerBuilder)
        {
            Infrastructure.DependencyInjection.Registration.Register(containerBuilder);
            Messaging.DependencyInjection.Registration.Register(containerBuilder);
            ReadSide.DependencyInjection.Registration.Register(containerBuilder);

        }
    }
}