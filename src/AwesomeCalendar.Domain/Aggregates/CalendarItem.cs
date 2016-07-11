using System;
using System.Collections.Generic;
using AwesomeCalendar.Contracts.Events;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;


namespace AwesomeCalendar.Domain.Aggregates
{
    public class CalendarItem : AggregateRoot, 
        IHandle<CalendarItemCreatedEvent>, 
        IHandle<CalendarItemCycleCreatedEvent>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
       
        List<CalendarItemCycle> Cycles { get; set; }


        public CalendarItem(Guid id, string description, DateTime startDate, DateTime endDate, IEnumerable<Contracts.Commands.CalendarItemCycle> cycles)
        {
            ApplyChange(new CalendarItemCreatedEvent
            {
                AggregateId = id,
                Description = description,
                StartDate = startDate,
                EndDate = endDate
            });

            foreach (var cycle in cycles)
                ApplyChange(new CalendarItemCycleCreatedEvent
                {
                    AggregateId = id,
                    EndTime = cycle.EndTime,
                    Interval = cycle.Interval,
                    Type = cycle.Type
                });
        }

        public CalendarItem()
        {
            
        }

        public void Handle(CalendarItemCreatedEvent @event)
        {
            Id = @event.AggregateId;
            Name = @event.Name;
            Description = @event.Description;
            StartDate = @event.StartDate;
            EndDate = @event.EndDate;
        }

        public void Handle(CalendarItemCycleCreatedEvent @event)
        {
            Cycles.Add(new CalendarItemCycle(@event.EndTime,@event.Interval, @event.Type));
        }
    }
}