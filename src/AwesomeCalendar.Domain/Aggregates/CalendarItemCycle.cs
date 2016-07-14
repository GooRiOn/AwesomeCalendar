using System;
using System.Collections.Generic;
using AwesomeCalendar.Infrastructure.Enums;

namespace AwesomeCalendar.Domain.Aggregates
{
    class CalendarItemCycle
    {
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Interval { get; set; }

        public CalendarItemCycleType Type { get; set; }

        public List<CalendarItemCycleExclusion> Exclusions { get; set; } = new List<CalendarItemCycleExclusion>();

        public CalendarItemCycle(DateTime startDate, DateTime? endDate, int interval, CalendarItemCycleType type)
        {
            StartDate = startDate;
            EndDate = endDate;
            Interval = interval;
            Type = type;
        }
    }
}