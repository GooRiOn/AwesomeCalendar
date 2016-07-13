using Autofac;
using AwesomeCalendar.ReadSide.Repositories;
using AwesomeCalendar.ReadSide.Repositories.Interfaces;

namespace AwesomeCalendar.ReadSide.DependencyInjection
{
    public class Registration
    {
        public static void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<ReadSideContext>().AsSelf();

            containerBuilder.RegisterType<CalendarItemRepository>().As<ICalendarItemRepository>();

            containerBuilder.RegisterType<CalendarItemCycleRepository>().As<ICalendarItemCycleRepository>();
        }
    }
}
