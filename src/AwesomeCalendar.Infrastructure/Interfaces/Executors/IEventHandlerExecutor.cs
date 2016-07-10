using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.Infrastructure.Interfaces.Executors
{
    public interface IEventHandlerExecutor
    {
        void Execute<TEvent>(TEvent @event) where TEvent : class, IEvent;
    }
}