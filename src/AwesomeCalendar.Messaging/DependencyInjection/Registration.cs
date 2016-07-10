using Autofac;
using AwesomeCalendar.Infrastructure.Interfaces.Busses;
using AwesomeCalendar.Messaging.Busses;

namespace AwesomeCalendar.Messaging.DependencyInjection
{
    public class Registration
    {
        public static void Register(ContainerBuilder containerBuilder)
        {
            Domain.DependencyInjection.Registration.Register(containerBuilder);

            containerBuilder.RegisterType<CommandBus>().As<ICommandBus>().SingleInstance();
        }
    }
}
