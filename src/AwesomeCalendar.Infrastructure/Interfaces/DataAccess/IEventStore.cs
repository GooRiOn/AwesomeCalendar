using System;
using System.Data.Entity;
using System.Threading.Tasks;
using AwesomeCalendar.Infrastructure.Interfaces.Aggragates;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.Infrastructure.Interfaces.DataAccess
{
    public interface IEventStore<TBaseEvent> where TBaseEvent : class, IEvent
    {
        Task PersistAsync<TAggregate>(TAggregate aggregate) where TAggregate : class, IAggregateRoot;

        Task<TAggregate> GetByIdAsync<TAggregate>(Guid id) where TAggregate : IAggregateRoot, new();
    }
}