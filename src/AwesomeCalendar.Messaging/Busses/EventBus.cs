using System;
using System.Threading.Tasks;
using AwesomeCalendar.Infrastructure.Interfaces.Busses;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;
using EasyNetQ;
using IEventBus = AwesomeCalendar.Infrastructure.Interfaces.Busses.IEventBus;

namespace AwesomeCalendar.Messaging.Busses
{
    public class EventBus : IEventBus
    {
        IBus Bus { get; }
        IEventBusExecutor EventBusExecutor { get; }

        public EventBus(IEventBusExecutor eventBusExecutor)
        {
            EventBusExecutor = eventBusExecutor;

            Bus = RabbitHutch.CreateBus("host=localhost");
            Bus.Receive(nameof(EventBus), (Action<IEvent>) EventBusExecutor.ExecuteAsync);
        }

        public async Task SendAsync<TEvent>(TEvent @event) where TEvent : class, IEvent =>
            await Bus.SendAsync(nameof(EventBus), @event);
    }
}