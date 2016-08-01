using Autofac;
using AwesomeCalendar.Contracts.Commands;
using AwesomeCalendar.Contracts.Events;
using AwesomeCalendar.Domain.CommandHandlers;
using AwesomeCalendar.Domain.EventHandlers;
using AwesomeCalendar.Domain.Factories;
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
            containerBuilder.RegisterType<EditCalendarItemCommandHandler>().As<ICommandHandler<EditCalendarItemCommand>>();
            containerBuilder.RegisterType<DeleteCalendarItemCommandHandler>().As<ICommandHandler<DeleteCalendarItemCommand>>();


            containerBuilder.RegisterType<EventHandlerExecutor>().As<IEventHandlerExecutor>();
            containerBuilder.RegisterType<CalendarItemCreatedEventHandler>().As<IEventHandler<CalendarItemCreatedEvent>>();
            containerBuilder.RegisterType<CalendarItemCycleCreatedEventHandler>().As<IEventHandler<CalendarItemCycleCreatedEvent>>();
            containerBuilder.RegisterType<CalendarItemDeletedEventHandler>().As<IEventHandler<CalendarItemDeletedEvent>>();
        }
    }
}
