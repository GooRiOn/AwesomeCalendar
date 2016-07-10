using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.Infrastructure.Interfaces.Handlers
{
    public interface IEventHandler<in TEvent> where TEvent : class, IEvent
    {
        void Handle(TEvent @event);
    }
}