using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using AwesomeCalendar.Contracts.Events;
using AwesomeCalendar.DataAccess.Database;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.DataAccess.Extensions
{
    internal static class EventStoreExtensions
    {
        static JavaScriptSerializer Serializer { get; } = new JavaScriptSerializer(); 

        internal static IEnumerable<EventStoreEntity> AsEventStoreEntities(this IEnumerable<IEvent> aggregateEvents) => 
            aggregateEvents.Select(aggregateEvent => new EventStoreEntity
            {
                AggregateId = aggregateEvent.AggregateId,
                Data = Serializer.Serialize(aggregateEvent),
                Type = aggregateEvent.GetType().ToString()
            });

        internal static IEnumerable<IEvent> AsAggregateEvents(this IEnumerable<EventStoreEntity> eventStoreEntities)
        {
            var serializerType = Serializer.GetType();

            foreach (var entity in eventStoreEntities)
            {
                var entityType = typeof(CalendarItemCreatedEvent).Assembly.GetType(entity.Type);

                yield return (IEvent) serializerType.GetMethod(nameof(Serializer.Deserialize), new[] { typeof(string) })
                    .MakeGenericMethod(entityType)
                    .Invoke(Serializer, new[] {entity.Data});
            }
        }
    }
}