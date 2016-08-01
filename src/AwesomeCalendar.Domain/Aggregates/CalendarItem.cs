using System;
using System.Collections.Generic;
using System.Linq;
using AwesomeCalendar.Contracts.Events;
using AwesomeCalendar.Infrastructure.Enums;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;


namespace AwesomeCalendar.Domain.Aggregates
{
    public class CalendarItem : AggregateRoot, 
        IHandle<CalendarItemCreatedEvent>, 
        IHandle<CalendarItemCycleCreatedEvent>,
        IHandle<CalendarItemEditedEvent>,
        IHandle<CalendarItemCycleEditedEvent>,
        IHandle<CycleExclusionCreatedEvent>
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
       
        List<CalendarItemCycle> Cycles { get; } = new List<CalendarItemCycle>();


        public CalendarItem(Guid id, string userId, string name, string description, 
            DateTime startDate, DateTime endDate, IEnumerable<Contracts.Commands.CalendarItemCycle> cycles)
        {
            ApplyChange(new CalendarItemCreatedEvent
            {
                AggregateId = id,
                UserId = userId,
                Name = name,
                Description = description,
                StartDate = startDate,
                EndDate = endDate
            });

            if (cycles == null) return;

            foreach (var cycle in cycles)
                ApplyChange(new CalendarItemCycleCreatedEvent
                {
                    AggregateId = id,
                    EndDate = cycle.EndDate,
                    Interval = cycle.Interval,
                    Type = cycle.Type,
                    StartDate = cycle.StartDate
                });
        }

        public CalendarItem()
        {
            
        }

        public void Edit(string userId, string name, string description, CalendarItemEditionType editionType,
            DateTime startDate, DateTime endDate, List<Contracts.Commands.CalendarItemCycle> cycles)
        {
            throw new NotImplementedException();
        }

        void IHandle<CalendarItemCreatedEvent>.Handle(CalendarItemCreatedEvent @event)
        {
            Id = @event.AggregateId;
            UserId = @event.UserId;
            Name = @event.Name;
            Description = @event.Description;
            StartDate = @event.StartDate;
            EndDate = @event.EndDate;
        }

        void IHandle<CalendarItemCycleCreatedEvent>.Handle(CalendarItemCycleCreatedEvent @event) =>
            Cycles.Add(new CalendarItemCycle(@event.StartDate, @event.EndDate,@event.Interval, @event.Type));

        void IHandle<CalendarItemEditedEvent>.Handle(CalendarItemEditedEvent @event)
        {
            Name = @event.Name;
            Description = @event.Description;
            StartDate = @event.StartDate;
            EndDate = @event.EndDate;
        }

        void IHandle<CalendarItemCycleEditedEvent>.Handle(CalendarItemCycleEditedEvent @event)
        {
            var cycle = Cycles.FirstOrDefault(c => c.StartDate.DayOfWeek == @event.StartDate.DayOfWeek);
            Cycles.Remove(cycle);

            cycle.Type = @event.Type;
            cycle.StartDate = @event.StartDate;
            cycle.EndDate = @event.EndDate;
            cycle.Interval = @event.Interval;

            Cycles.Add(cycle);
        }

        void IHandle<CycleExclusionCreatedEvent>.Handle(CycleExclusionCreatedEvent @event)
        {
            var cycle = Cycles.FirstOrDefault(c => c.StartDate.DayOfWeek == @event.StartDate.DayOfWeek);

            cycle.Exclusions.Add(new CalendarItemCycleExclusion
            (
                Name = @event.Name,
                Description = @event.Description,
                StartDate = @event.StartDate,
                EndDate = @event.EndDate
            ));
        }
    }
}