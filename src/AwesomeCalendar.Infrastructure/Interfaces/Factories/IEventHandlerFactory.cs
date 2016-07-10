using AwesomeCalendar.Infrastructure.Interfaces.Contracts;
using AwesomeCalendar.Infrastructure.Interfaces.Handlers;

namespace AwesomeCalendar.Infrastructure.Interfaces.Factories
{
    public interface IEventHandlerFactory
    {
        IEventHandler<TEvent> Get<TEvent>() where TEvent : class, IEvent;
    }
}