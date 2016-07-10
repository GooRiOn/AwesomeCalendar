using System;
using System.Collections.Generic;
using AwesomeCalendar.Infrastructure.Interfaces.Aggragates;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.Domain.Aggregates
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        public Guid Id { get; set; }

        public int Version { get; set; }

        public List<IEvent> GetUncommittedEvents() => Events;

        public void MarkEventsAsCommitted() => Events.Clear();

        protected List<IEvent> Events { get; set; } = new List<IEvent>();

        public virtual void LoadFromHistory(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
                ApplyChange(@event);
        }

        protected void ApplyChange(IEvent @event)
        {
            var aggregateType = GetType();

            var eventType = @event.GetType();

            aggregateType.GetMethod(nameof(IHandle<IEvent>.Handle), new[] { eventType })
                    .Invoke(this, new object[] { @event });
        }
    }
}