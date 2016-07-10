using Autofac;

namespace AwesomeCalendar.Messaging.DependencyInjection
{
    public class Registration
    {
        public static void Register(ContainerBuilder containerBuilder)
        {
            Domain.DependencyInjection.Registration.Register(containerBuilder);
        }
    }
}
