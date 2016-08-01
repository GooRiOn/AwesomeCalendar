using System;
using System.Collections.Generic;
using System.Linq;
using AwesomeCalendar.Contracts.Commands;
using AwesomeCalendar.Domain.Aggregates;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace AwesomeCalendar.Tests.Aggregates
{
    public class calendar_item_creation_tests
    {
        CalendarItem CalendarItem { get; set; }

        void act(CreateCalendarItemCommand command)
        {
            CalendarItem = new CalendarItem(command.Id, command.UserId, command.Name, command.Description, command.StartDate, command.EndDate, command.Cycles);
        }

        [Theory]
        [AutoData]
        public void creates_proper_number_of_events_on_creating(CreateCalendarItemCommand command )
        {
            act(command);

            Assert.Equal(command.Cycles.Count() + 1, CalendarItem.GetUncommittedEvents().Count);
        }

        [Theory]
        [AutoData]
        public void sets_correct_calendar_item_properties(CreateCalendarItemCommand command)
        {
            act(command);

            Assert.Equal(command.Id, CalendarItem.Id);
            Assert.Equal(command.Name, CalendarItem.Name);
            Assert.Equal(command.UserId, CalendarItem.UserId);
            Assert.Equal(command.Description, CalendarItem.Description);
            Assert.Equal(command.StartDate, CalendarItem.StartDate);
            Assert.Equal(command.EndDate, CalendarItem.EndDate);
        }

        [Theory]
        [AutoData]
        public void sets_correct_calendar_item_cycles_properties(CreateCalendarItemCommand command)
        {
            act(command);

            var commandItemCycle = command.Cycles.First();
            var calendarItemCycle = CalendarItem.Cycles.First();

            Assert.Equal(commandItemCycle.Interval, calendarItemCycle.Interval);
            Assert.Equal(commandItemCycle.Type, calendarItemCycle.Type);
            Assert.Equal(commandItemCycle.StartDate, calendarItemCycle.StartDate);
            Assert.Equal(commandItemCycle.EndDate, calendarItemCycle.EndDate);
        }
    }
}
