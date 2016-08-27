using System;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.Contracts.Events
{
    public class CalendarItemCycleDeletedEvent : IEvent
    {
        public Guid CycleId { get; set; }

        public Guid AggregateId { get; set; }
    }
}
