using System;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.Contracts.Events
{
    public abstract class CalendarItemBaseEvent : IEvent
    {
        public Guid Id { get; set; }

        public Guid AggregateId { get; set; }

        public DateTime CreatedDate { get; }

        public DateTime StartDate { get; set; }

        protected CalendarItemBaseEvent()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.UtcNow;
        }
    }
}