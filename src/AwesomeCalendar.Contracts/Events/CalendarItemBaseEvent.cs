using System;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.Contracts.Events
{
    public class CalendarItemBaseEvent : IEvent
    {
        public Guid Id { get; set; }

        public Guid AggregateId { get; set; }

        public CalendarItemBaseEvent()
        {
            Id = Guid.NewGuid();
        }
    }
}