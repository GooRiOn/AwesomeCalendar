using System;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.Contracts.Events
{
    public class CycleExclusionCreatedEvent : IEvent
    {
        public Guid AggregateId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}