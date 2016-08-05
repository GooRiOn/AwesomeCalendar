using System.Threading.Tasks;
using AwesomeCalendar.Infrastructure.Interfaces.Busses;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;
using AwesomeCalendar.Infrastructure.Interfaces.Executors;

namespace AwesomeCalendar.Messaging.Executors
{
    public class EventBusExecutor : IEventBusExecutor
    {
        IEventHandlerExecutor EventHandlerExecutor { get; }

        public EventBusExecutor(IEventHandlerExecutor eventHandlerExecutor)
        {
            EventHandlerExecutor = eventHandlerExecutor;
        }

        public async void ExecuteAsync(IEvent @event)
        {
            var eventType = @event.GetType();
            var executorType = EventHandlerExecutor.GetType();

            await (Task) executorType.GetMethod(nameof(IEventBusExecutor.ExecuteAsync))
                .MakeGenericMethod(eventType)
                .Invoke(EventHandlerExecutor, new[] { @event });
        }
    }
}