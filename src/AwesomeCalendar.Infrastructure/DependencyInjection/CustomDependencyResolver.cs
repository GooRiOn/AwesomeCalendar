using Autofac;
using AwesomeCalendar.Infrastructure.DependencyInjection.Interfaces;

namespace AwesomeCalendar.Infrastructure.DependencyInjection
{
    public class CustomDependencyResolver : ICustomDependencyResolver
    {
        IContainer Container { get; }

        public CustomDependencyResolver(IContainer container)
        {
            Container = container;
        }

        public TResolved Resolve<TResolved>() => Container.Resolve<TResolved>();
    }
}