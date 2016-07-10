using Autofac;
using AwesomeCalendar.Infrastructure.DependencyInjection.Interfaces;

namespace AwesomeCalendar.Infrastructure.DependencyInjection
{
    public class Registration
    {
        public static void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<CustomDependencyResolver>().As<ICustomDependencyResolver>();
        }
    }
}
