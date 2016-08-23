using Autofac;
using AwesomeCalendar.DataAccess.Database;
using AwesomeCalendar.Infrastructure.Interfaces.DataAccess;

namespace AwesomeCalendar.DataAccess.DependencyInjection
{
    public class Registration
    {
        public static void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<EventStoreContext>().AsSelf();
            containerBuilder.RegisterType<EventStore>().As<IEventStore>();
        }
    }
}
