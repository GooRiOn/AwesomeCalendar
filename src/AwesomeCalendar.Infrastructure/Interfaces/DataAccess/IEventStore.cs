using System;
using System.Threading.Tasks;
using AwesomeCalendar.Infrastructure.Interfaces.Aggragates;

namespace AwesomeCalendar.Infrastructure.Interfaces.DataAccess
{
    public interface IEventStore
    {
        Task PersistAsync<TAggregate>(TAggregate aggregate) where TAggregate : class, IAggregateRoot;

        Task<TAggregate> GetByIdAsync<TAggregate>(Guid id) where TAggregate : IAggregateRoot, new();
    }
}