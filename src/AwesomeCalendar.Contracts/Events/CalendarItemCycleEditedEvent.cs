using System;
using AwesomeCalendar.Infrastructure.Enums;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.Contracts.Events
{
    public class CalendarItemCycleEditedEvent : IEvent
    {
        public Guid CycleId { get; set; }

        public Guid AggregateId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public CalendarItemCycleType Type { get; set; }

        public int Interval { get; set; }
    }
}