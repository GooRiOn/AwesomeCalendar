using System.Linq;
using Autofac;
using AwesomeCalendar.Contracts.Events;
using AwesomeCalendar.Infrastructure.Interfaces.DataAccess;

namespace AwesomeCalendar.DataAccess.DependencyInjection
{
    public class Registration
    {
        public static void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<EventStoreContext>().AsSelf();
            containerBuilder.RegisterGeneric(typeof(EventStore<>)).As(typeof(IEventStore<>));
        }
    }
}
