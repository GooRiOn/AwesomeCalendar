using Autofac;
using AwesomeCalendar.Contracts.Commands;
using AwesomeCalendar.Domain.Factories;
using AwesomeCalendar.Domain.Handlers;
using AwesomeCalendar.Infrastructure.Interfaces.Executors;
using AwesomeCalendar.Infrastructure.Interfaces.Handlers;

namespace AwesomeCalendar.Domain.DependencyInjection
{
    public class Registration
    {
        public static void Register(ContainerBuilder containerBuilder)
        {
            DataAccess.DependencyInjection.Registration.Register(containerBuilder);

            containerBuilder.RegisterType<CommandHandlerExecutor>().As<ICommandHandlerExecutor>();
            containerBuilder.RegisterType<CreateCalendarItemCommandHandler>().As<ICommandHandler<CreateCalendarItemCommand>>();


            containerBuilder.RegisterType<EventHandlerExecutor>().As<IEventHandlerExecutor>();
        }
    }
}
