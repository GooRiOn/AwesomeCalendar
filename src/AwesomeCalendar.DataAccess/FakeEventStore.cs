using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCalendar.Infrastructure.Interfaces.Aggragates;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;
using AwesomeCalendar.Infrastructure.Interfaces.DataAccess;

namespace AwesomeCalendar.DataAccess
{
    public class FakeEventStore : IEventStore
    {
        private ICollection<IEvent> EventStore { get; } = new List<IEvent>();

        public async Task PersistAsync<TAggregate>(TAggregate aggregate) where TAggregate : class, IAggregateRoot
        {
            var events = aggregate.GetUncommittedEvents();

            foreach (var @event in events)
                EventStore.Add(@event);
        }

        public async Task<TAggregate> GetByIdAsync<TAggregate, TEvent>(Guid id) where TAggregate : IAggregateRoot, new() where TEvent : class, IEvent
        {
            var events = EventStore.Where(e => e.AggregateId == id).ToList();
            var aggreagte = new TAggregate();

            aggreagte.LoadFromHistory(events);

            return aggreagte;
        }
    }
}