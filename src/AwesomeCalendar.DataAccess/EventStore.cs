using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCalendar.Infrastructure.Enums;
using AwesomeCalendar.Infrastructure.Exceptions;
using AwesomeCalendar.Infrastructure.Interfaces.Aggragates;
using AwesomeCalendar.Infrastructure.Interfaces.Busses;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;
using AwesomeCalendar.Infrastructure.Interfaces.DataAccess;

namespace AwesomeCalendar.DataAccess
{
    public class EventStore<TBaseEvent> : IEventStore<TBaseEvent> where TBaseEvent : class, IEvent
    {
        EventStoreContext Context { get; }
        IEventBus EventBus { get; }

        static readonly object EventStoreLocker = new object();
        
        public EventStore(EventStoreContext context, IEventBus eventBus)
        {
            Context = context;
            EventBus = eventBus;
        }

        public async Task PersistAsync<TAggregate>(TAggregate aggregate) where TAggregate : class, IAggregateRoot
        {
            var events = aggregate.GetUncommittedEvents();

            lock (EventStoreLocker)
            {
                var aggreagteVersion = aggregate.Version;

                if(aggreagteVersion != -1)
                    CheckAggregateVersion(aggregate.Id, aggreagteVersion);

                foreach (var @event in events)
                {
                    @event.Version = ++aggreagteVersion;

                    var eventType = @event.GetType();

                    var genericSetMethod = Context.GetType().GetMethods().First(m => m.Name == "Set" && m.IsGenericMethod);

                    var dbSet = genericSetMethod.MakeGenericMethod(eventType).Invoke(Context, null);

                    dbSet.GetType().GetMethod("Add").Invoke(dbSet, new[] { @event });
                }
            }

            await Context.SaveChangesAsync();

            foreach (var @event in events)
                await EventBus.SendAsync(@event);

        }

        public async Task<TAggregate> GetByIdAsync<TAggregate>(Guid id) where TAggregate : IAggregateRoot, new()
        {
            var events = await Context.Set<TBaseEvent>().Where(e => e.AggregateId == id).AsNoTracking().OrderBy(e => e.CreatedDate).ToListAsync();

            if(!events.Any()) 
                throw new AwesomeCalendarException(AwesomeCalendarExceptionType.AggregateNotFound, typeof(TAggregate));

            var aggragate = new TAggregate();
            aggragate.LoadFromHistory(events);

            return aggragate;
        }

        private void CheckAggregateVersion(Guid aggreagteId, int expectedVersion)
        {
            var currentAggreagteVersion = Context.Set<TBaseEvent>().Where(e => e.AggregateId == aggreagteId).Max(e => e.Version);

            if(expectedVersion != currentAggreagteVersion)
                throw new AwesomeCalendarException(AwesomeCalendarExceptionType.EventStoreConcurency, typeof(TBaseEvent));
        }
    }
}