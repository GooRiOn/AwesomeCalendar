using Autofac;
using AwesomeCalendar.Domain.Factories;
using AwesomeCalendar.Infrastructure.Interfaces.Executors;

namespace AwesomeCalendar.Domain.DependencyInjection
{
    public class Registration
    {
        public static void Register(ContainerBuilder containerBuilder)
        {
            DataAccess.DependencyInjection.Registration.Register(containerBuilder);

            containerBuilder.RegisterType<CommandHandlerExecutor>().As<ICommandHandlerExecutor>();


            containerBuilder.RegisterType<EventHandlerExecutor>().As<IEventHandlerExecutor>();
        }
    }
}
