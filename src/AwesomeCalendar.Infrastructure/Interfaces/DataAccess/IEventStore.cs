using System;
using AwesomeCalendar.Infrastructure.Interfaces.Aggragates;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.Infrastructure.Interfaces.DataAccess
{
    public interface IEventStore
    {
        void Persist<TAggregate>(TAggregate aggregate) where TAggregate : class, IAggregateRoot;

        TAggregate GetById<TAggregate, TEvent>(Guid id)
            where TAggregate : IAggregateRoot, new()
            where TEvent : class, IEvent;
    }
}