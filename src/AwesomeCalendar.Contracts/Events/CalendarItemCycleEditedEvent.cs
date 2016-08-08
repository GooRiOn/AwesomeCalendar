using System;
using AwesomeCalendar.Infrastructure.Enums;

namespace AwesomeCalendar.Contracts.Events
{
    public class CalendarItemCycleEditedEvent : CalendarItemBaseEvent
    {
        public DateTime? EndDate { get; set; }

        public CalendarItemCycleType Type { get; set; }

        public int Interval { get; set; }
    }
}