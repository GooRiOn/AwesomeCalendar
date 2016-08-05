using System;
using System.Threading.Tasks;
using AwesomeCalendar.Infrastructure.Interfaces.Aggragates;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.Infrastructure.Interfaces.DataAccess
{
    public interface IEventStore
    {
        Task PersistAsync<TAggregate>(TAggregate aggregate) where TAggregate : class, IAggregateRoot;

        Task<TAggregate> GetByIdAsync<TAggregate, TEvent>(Guid id) where TAggregate : IAggregateRoot, new() where TEvent : class, IEvent;

    }
}