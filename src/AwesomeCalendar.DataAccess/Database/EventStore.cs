using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCalendar.DataAccess.Extensions;
using AwesomeCalendar.Infrastructure.Enums;
using AwesomeCalendar.Infrastructure.Exceptions;
using AwesomeCalendar.Infrastructure.Interfaces.Aggragates;
using AwesomeCalendar.Infrastructure.Interfaces.Busses;
using AwesomeCalendar.Infrastructure.Interfaces.DataAccess;

namespace AwesomeCalendar.DataAccess.Database
{
    public class EventStore : IEventStore
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
            var eventStoreEntities = aggregate.GetUncommittedEvents().AsEventStoreEntities();

            lock (EventStoreLocker)
            {
                var aggreagteVersion = aggregate.Version;

                if(aggreagteVersion != -1)
                    CheckAggregateVersion(aggregate.Id, aggreagteVersion);

                foreach (var @event in eventStoreEntities)
                {
                    @event.Version = ++aggreagteVersion;
                    Context.Events.Add(@event);
                }
            }

            await Context.SaveChangesAsync();

            foreach (var @event in aggregate.GetUncommittedEvents())
                await EventBus.SendAsync(@event);

        }

        public async Task<TAggregate> GetByIdAsync<TAggregate>(Guid id) where TAggregate : IAggregateRoot, new()
        {
            var events = await Context.Events.Where(e => e.AggregateId == id).AsNoTracking().OrderBy(e => e.CreatedDate).ToListAsync();

            if(!events.Any()) 
                throw new AwesomeCalendarException(AwesomeCalendarExceptionType.AggregateNotFound, typeof(TAggregate));

            var aggragate = new TAggregate();
            aggragate.LoadFromHistory(events.AsAggregateEvents());

            return aggragate;
        }

        private void CheckAggregateVersion(Guid aggreagteId, int expectedVersion)
        {
            var currentAggreagteVersion = Context.Events.Where(e => e.AggregateId == aggreagteId).Max(e => e.Version);

            if(expectedVersion != currentAggreagteVersion)
                throw new AwesomeCalendarException(AwesomeCalendarExceptionType.EventStoreConcurency);
        }
    }
}