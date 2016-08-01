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
    public class delete_calendar_item_command_handler_tests
    {
        private IEventStore EventStore { get; }
        private ICommandHandler<CreateCalendarItemCommand> CreateCommandHandler { get; }
        private ICommandHandler<DeleteCalendarItemCommand> DeleteCommandHandler { get; }

        public delete_calendar_item_command_handler_tests()
        {
            EventStore = new FakeEventStore();
            CreateCommandHandler = new CreateCalendarItemCommandHandler(EventStore);
            DeleteCommandHandler = new DeleteCalendarItemCommandHandler(EventStore);
        }

        void act(DeleteCalendarItemCommand command)
        {
            DeleteCommandHandler.Handle(command);
        }

        [Theory, AutoData]
        public void persists_event_to_event_store_needed_to_reconstruct_calendar_item(CreateCalendarItemCommand createCommand, DeleteCalendarItemCommand deleteCommand)
        {
            createCommand.EndDate = createCommand.StartDate.AddDays(1);
            CreateCommandHandler.Handle(createCommand);

            deleteCommand.Id = createCommand.Id;

            act(deleteCommand);
            var deletedAggreagte = EventStore.GetById<CalendarItem, CalendarItemBaseEvent>(deleteCommand.Id);

            deletedAggreagte.ShouldBeOfType(typeof(CalendarItem));
            Assert.Equal(deleteCommand.Id, deletedAggreagte.Id);
        }

        [Fact]
        public void throws_when_command_is_null()
        {
            var exception = Record.Exception(() => act(null));

            exception.ShouldBeOfType(typeof(AwesomeCalendarException));
            Assert.Equal(((AwesomeCalendarException)exception).Type, AwesomeCalendarExceptionType.NullCommand);
        }

        [Theory, AutoData]
        public void throws_when_command_has_empty_id(DeleteCalendarItemCommand command)
        {
            command.Id = Guid.Empty;

            var exception = Record.Exception(() => act(command));

            exception.ShouldBeOfType(typeof(AwesomeCalendarException));
            Assert.Equal(((AwesomeCalendarException)exception).Type, AwesomeCalendarExceptionType.InvalidCommand);
        }

        [Theory, AutoData]
        public void throws_when_command_not_found(CreateCalendarItemCommand createCommand, DeleteCalendarItemCommand deleteCommand)
        {
            createCommand.EndDate = createCommand.StartDate.AddDays(1);
            CreateCommandHandler.Handle(createCommand);

            var exception = Record.Exception(() => act(deleteCommand));

            exception.ShouldBeOfType(typeof(AwesomeCalendarException));
        }
    }
}
