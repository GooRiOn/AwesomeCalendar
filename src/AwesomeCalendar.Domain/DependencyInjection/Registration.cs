using Autofac;
using AwesomeCalendar.Domain.Factories;
using AwesomeCalendar.Infrastructure.Interfaces.Factories;

namespace AwesomeCalendar.Domain.DependencyInjection
{
    public class Registration
    {
        public static void Register(ContainerBuilder containerBuilder)
        {
            DataAccess.DependencyInjection.Registration.Register(containerBuilder);

            containerBuilder.RegisterType<CommandHandlerFactory>().As<ICommandHandlerFactory>();


            containerBuilder.RegisterType<EventHandlerExecutor>().As<IEventHandlerExecutor>();
        }
    }
}
