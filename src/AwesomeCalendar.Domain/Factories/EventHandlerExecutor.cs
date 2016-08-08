using System.Threading.Tasks;
using AwesomeCalendar.Infrastructure.DependencyInjection.Interfaces;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;
using AwesomeCalendar.Infrastructure.Interfaces.Executors;
using AwesomeCalendar.Infrastructure.Interfaces.Handlers;

namespace AwesomeCalendar.Domain.Factories
{
    public class EventHandlerExecutor : IEventHandlerExecutor
    {
        ICustomDependencyResolver CustomDependencyResolver { get; }

        public EventHandlerExecutor(ICustomDependencyResolver customDependencyResolver)
        {
            CustomDependencyResolver = customDependencyResolver;
        }

        public async Task ExecuteAsync<TEvent>(TEvent @event) where TEvent : class, IEvent =>
            await CustomDependencyResolver.Resolve<IEventHandler<TEvent>>().HandleAsync(@event);
    }
}
