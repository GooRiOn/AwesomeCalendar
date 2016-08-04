using Autofac;
using Autofac.Extras.DynamicProxy2;
using AwesomeCalendar.Infrastructure.Interfaces.Busses;
using AwesomeCalendar.Messaging.Aspects;
using AwesomeCalendar.Messaging.Busses;
using AwesomeCalendar.Messaging.Executors;

namespace AwesomeCalendar.Messaging.DependencyInjection
{
    public class Registration
    {
        public static void Register(ContainerBuilder containerBuilder)
        {
            Domain.DependencyInjection.Registration.Register(containerBuilder);

            containerBuilder.RegisterType<CommandBus>().As<ICommandBus>().SingleInstance();

            containerBuilder.RegisterType<EventBus>().As<IEventBus>().SingleInstance();

            containerBuilder.RegisterType<CommandBusExecutor>().As<ICommandBusExecutor>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(HandleResultAspect));

            containerBuilder.RegisterType<HandleResultAspect>();
        }
    }
}
