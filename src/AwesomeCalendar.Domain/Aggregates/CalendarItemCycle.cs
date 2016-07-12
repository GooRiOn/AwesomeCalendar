using System;
using AwesomeCalendar.Infrastructure.Enums;

namespace AwesomeCalendar.Domain.Aggregates
{
    class CalendarItemCycle
    {
        public DateTime? EndDate { get; set; }

        public int Interval { get; set; }

        public CalendarItemCycleType Type { get; set; }

        public CalendarItemCycle(DateTime? endDate, int interval, CalendarItemCycleType type)
        {
            EndDate = endDate;
            Interval = interval;
            Type = type;
        }
    }
}