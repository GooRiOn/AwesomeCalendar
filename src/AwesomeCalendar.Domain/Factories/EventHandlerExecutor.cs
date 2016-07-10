using AwesomeCalendar.Infrastructure.DependencyInjection.Interfaces;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;
using AwesomeCalendar.Infrastructure.Interfaces.Factories;
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

        public void Execute<TEvent>(TEvent @event) where TEvent : class, IEvent =>
            CustomDependencyResolver.Resolve<IEventHandler<TEvent>>().Handle(@event);
    }
}
