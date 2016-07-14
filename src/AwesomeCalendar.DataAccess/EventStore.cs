using System;
using System.Data.Entity;
using System.Linq;
using AwesomeCalendar.Infrastructure.Interfaces.Aggragates;
using AwesomeCalendar.Infrastructure.Interfaces.Busses;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;
using AwesomeCalendar.Infrastructure.Interfaces.DataAccess;

namespace AwesomeCalendar.DataAccess
{
    public class EventStore : IEventStore
    {
        EventStoreContext Context { get; }
        IEventBus EventBus { get; }

        public EventStore(EventStoreContext context, IEventBus eventBus)
        {
            Context = context;
            EventBus = eventBus;
        }

        public void Persist<TAggregate>(TAggregate aggregate) where TAggregate : class, IAggregateRoot
        {
            var events = aggregate.GetUncommittedEvents();

            foreach (var @event in events)
            {
                var eventType = @event.GetType();

                var genericSetMethod = Context.GetType().GetMethods().First(m => m.Name == "Set" && m.IsGenericMethod);

                var dbSet = genericSetMethod.MakeGenericMethod(eventType).Invoke(Context, null);

                dbSet.GetType().GetMethod("Add").Invoke(dbSet, new[] { @event });
            }

            Context.SaveChanges();

            foreach (var @event in events)
                EventBus.Send(@event);
        }

        public TAggregate GetById<TAggregate,TEvent>(Guid id) 
            where TAggregate : IAggregateRoot, new()
            where TEvent : class, IEvent
            
        {
            var events = Context.Set<TEvent>().Where(e => e.AggregateId == id).AsNoTracking().ToList();

            var aggragate = new TAggregate();
            aggragate.LoadFromHistory(events);

            return aggragate;
        }
    }
}