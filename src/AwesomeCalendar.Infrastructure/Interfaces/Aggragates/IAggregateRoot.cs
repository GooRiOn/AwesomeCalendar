using System;
using System.Collections.Generic;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.Infrastructure.Interfaces.Aggragates
{
    public interface IAggregateRoot
    {
        Guid Id { get; set; }

        int Version { get; set; }

        List<IEvent> GetUncommittedEvents();

        void MarkEventsAsCommitted();

        void LoadFromHistory(IEnumerable<IEvent> events);
    }
}