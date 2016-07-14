using System;
using System.Linq;
using AwesomeCalendar.Contracts.Commands;
using AwesomeCalendar.Contracts.Events;
using AwesomeCalendar.DataAccess;
using AwesomeCalendar.Domain.Aggregates;
using AwesomeCalendar.Domain.CommandHandlers;
using AwesomeCalendar.Infrastructure.Enums;
using AwesomeCalendar.Infrastructure.Exceptions;
using AwesomeCalendar.Infrastructure.Interfaces.DataAccess;
using AwesomeCalendar.Infrastructure.Interfaces.Handlers;
using Ploeh.AutoFixture.Xunit2;
using Shouldly;
using Xunit;

namespace AwesomeCalendar.Tests.CommandHandlers
{
    public class create_calendar_item_command_handler_tests
    {
        ICommandHandler<CreateCalendarItemCommand> CommandHandler { get; }
        IEventStore EventStore { get; }

        public create_calendar_item_command_handler_tests()
        {
            EventStore = new FakeEventStore();
            CommandHandler = new CreateCalendarItemCommandHandler(EventStore);
        }

        void act(CreateCalendarItemCommand command)
        {
            CommandHandler.Handle(command);
        }

        [Theory, AutoData]
        public void persists_event_to_event_store_needed_to_reconstruct_calendar_item(CreateCalendarItemCommand command)
        {
            act(command);
            var createdAggreagte = EventStore.GetById<CalendarItem, CalendarItemBaseEvent>(command.Id);

            createdAggreagte.ShouldBeOfType(typeof(CalendarItem));
            Assert.Equal(command.Id, createdAggreagte.Id); 
        }

        [Fact]
        public void throws_when_command_is_null()
        {
            var exception = Record.Exception(() => act(null));

            exception.ShouldBeOfType(typeof(AwesomeCalendarException));
            Assert.Equal(((AwesomeCalendarException)exception).Type,AwesomeCalendarExceptionType.NullCommand);
        }

        [Theory, AutoData]
        public void throws_when_command_has_empty_id(CreateCalendarItemCommand command)
        {
            command.Id = Guid.Empty;
            
            var exception = Record.Exception(() => act(command));

            exception.ShouldBeOfType(typeof(AwesomeCalendarException));
            Assert.Equal(((AwesomeCalendarException)exception).Type, AwesomeCalendarExceptionType.InvalidCommand);
        }

        [Theory, AutoData]
        public void throws_when_command_has_empty_user_id(CreateCalendarItemCommand command)
        {
            command.UserId = String.Empty;

            var exception = Record.Exception(() => act(command));

            exception.ShouldBeOfType(typeof(AwesomeCalendarException));
            Assert.Equal(((AwesomeCalendarException)exception).Type, AwesomeCalendarExceptionType.InvalidCommand);
        }

        [Theory, AutoData]
        public void throws_when_command_has_empty_name(CreateCalendarItemCommand command)
        {
            command.Name = String.Empty;

            var exception = Record.Exception(() => act(command));

            exception.ShouldBeOfType(typeof(AwesomeCalendarException));
            Assert.Equal(((AwesomeCalendarException)exception).Type, AwesomeCalendarExceptionType.InvalidCommand);
        }

        [Theory, AutoData]
        public void throws_when_commands_end_date_is_earlier_than_start_date(CreateCalendarItemCommand command)
        {
            command.StartDate = DateTime.MaxValue;
            command.EndDate = DateTime.MinValue;

            var exception = Record.Exception(() => act(command));

            exception.ShouldBeOfType(typeof(AwesomeCalendarException));
            Assert.Equal(((AwesomeCalendarException)exception).Type, AwesomeCalendarExceptionType.InvalidCommand);
        }
    }
}