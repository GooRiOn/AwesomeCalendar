using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task PersistAsync<TAggregate>(TAggregate aggregate) where TAggregate : class, IAggregateRoot
        {
            var events = aggregate.GetUncommittedEvents();

            foreach (var @event in events)
            {
                var eventType = @event.GetType();

                var genericSetMethod = Context.GetType().GetMethods().First(m => m.Name == "Set" && m.IsGenericMethod);

                var dbSet = genericSetMethod.MakeGenericMethod(eventType).Invoke(Context, null);

                dbSet.GetType().GetMethod("Add").Invoke(dbSet, new[] { @event });
            }

            await Context.SaveChangesAsync();

            foreach (var @event in events)
                await EventBus.SendAsync(@event);
        }

        public async Task<TAggregate> GetByIdAsync<TAggregate,TEvent>(Guid id) 
            where TAggregate : IAggregateRoot, new()
            where TEvent : class, IEvent
            
        {
            var events = await Context.Set<TEvent>().Where(e => e.AggregateId == id).AsNoTracking().OrderBy(e => e.CreatedDate).ToListAsync();

            var aggragate = new TAggregate();
            aggragate.LoadFromHistory(events);

            return aggragate;
        }
    }
}