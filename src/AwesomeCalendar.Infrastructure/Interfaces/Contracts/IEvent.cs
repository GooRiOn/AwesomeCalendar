using System;

namespace AwesomeCalendar.Infrastructure.Interfaces.Contracts
{
    public interface IEvent
    {
        Guid AggregateId { get; }
        DateTime CreatedDate { get; }
    }
}