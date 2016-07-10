using System;
using System.Threading.Tasks;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;
using AwesomeCalendar.Infrastructure.Interfaces.Executors;
using EasyNetQ;
using IEventBus = AwesomeCalendar.Infrastructure.Interfaces.Busses.IEventBus;

namespace AwesomeCalendar.Messaging.Busses
{
    public class EventBus : IEventBus
    {
        IBus Bus { get; }
        IEventHandlerExecutor EventHandlerExecutor { get; }

        public EventBus( IEventHandlerExecutor eventHandlerExecutor)
        {
            EventHandlerExecutor = eventHandlerExecutor;

            Bus = RabbitHutch.CreateBus("host=localhost");
            Bus.Receive(nameof(EventBus), (Action<IEvent>) ProccessBus);
        }

        public void Send<TEvent>(TEvent @event) where TEvent : class, IEvent =>
            Bus.Publish(@event);


        public async Task SendAsync<TEvent>(TEvent @event) where TEvent : class, IEvent =>
            await Bus.PublishAsync(@event);


        void ProccessBus(IEvent @event)
        {
            var eventType = @event.GetType();
            var executorType = EventHandlerExecutor.GetType();

            executorType.GetMethod(nameof(ICommandHandlerExecutor.Execute))
                .MakeGenericMethod(eventType)
                .Invoke(EventHandlerExecutor, new[] { @event });
        }
    }
}