using System;
using System.Collections.Generic;
using System.Linq;
using AwesomeCalendar.Contracts.Events;
using AwesomeCalendar.Infrastructure.Enums;
using AwesomeCalendar.Infrastructure.Exceptions;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;


namespace AwesomeCalendar.Domain.Aggregates
{
    public class CalendarItem : AggregateRoot, 
        IHandle<CalendarItemCreatedEvent>, 
        IHandle<CalendarItemCycleCreatedEvent>,
        IHandle<CalendarItemEditedEvent>,
        IHandle<CalendarItemCycleEditedEvent>,
        IHandle<CalendarItemDeletedEvent>,
        IHandle<CalendarItemCycleDeletedEvent>,
        IHandle<CycleExclusionCreatedEvent>
    {
        public string UserId { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }
       
        public List<CalendarItemCycle> Cycles { get; private set; } = new List<CalendarItemCycle>();


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

        public void Delete() => 
            Events.Add(new CalendarItemDeletedEvent {AggregateId = Id}); // not sure about that

        public void DeleteCycle(Guid cycleId)
        {
            Events.Add(new CalendarItemCycleDeletedEvent {AggregateId = Id, CycleId = cycleId});
            Cycles.RemoveAll(c => c.Id == cycleId);
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
            var cycle = Cycles.FirstOrDefault(c => c.Id == @event.CycleId);
            Cycles.Remove(cycle);

            cycle.Type = @event.Type;
            cycle.StartDate = @event.StartDate;
            cycle.EndDate = @event.EndDate;
            cycle.Interval = @event.Interval;

            Cycles.Add(cycle);
        }

        void IHandle<CalendarItemDeletedEvent>.Handle(CalendarItemDeletedEvent @event)
        {
            throw new AwesomeCalendarException(AwesomeCalendarExceptionType.AggregateDeleted, typeof(CalendarItem));
        }

        void IHandle<CalendarItemCycleDeletedEvent>.Handle(CalendarItemCycleDeletedEvent @event)
        {
            Cycles.RemoveAll(c => c.Id == @event.CycleId);
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