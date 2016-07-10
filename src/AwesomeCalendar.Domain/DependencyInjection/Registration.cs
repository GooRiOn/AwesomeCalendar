using Autofac;

namespace AwesomeCalendar.Domain.DependencyInjection
{
    public class Registration
    {
        public static void Register(ContainerBuilder containerBuilder)
        {
            DataAccess.DependencyInjection.Registration.Register(containerBuilder);
        }
    }
}
