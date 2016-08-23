using System;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.Contracts.Events
{
    public class CalendarItemDeletedEvent : IEvent
    {
        public Guid AggregateId { get; set; }
    }
}
