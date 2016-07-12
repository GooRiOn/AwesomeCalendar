using System;

namespace AwesomeCalendar.Contracts.Events
{
    public class CalendarItemCreatedEvent : CalendarItemBaseEvent
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}