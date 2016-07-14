using System;

namespace AwesomeCalendar.Domain.Aggregates
{
    public class CalendarItemCycleExclusion
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public CalendarItemCycleExclusion(string name, string description, DateTime startDate, DateTime endDate)
        {
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}