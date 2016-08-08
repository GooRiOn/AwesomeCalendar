using System;
using System.Linq;
using System.Threading.Tasks;
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
        IEventStore EventStore { get; }
        ICommandHandler<CreateCalendarItemCommand> CreateCommandHandler { get; }
        ICommandHandler<DeleteCalendarItemCommand> DeleteCommandHandler { get; }

        public delete_calendar_item_command_handler_tests()
        {
            EventStore = new FakeEventStore();
            CreateCommandHandler = new CreateCalendarItemCommandHandler(EventStore);
            DeleteCommandHandler = new DeleteCalendarItemCommandHandler(EventStore);
        }

        async Task act(DeleteCalendarItemCommand command)
        {
            await DeleteCommandHandler.HandleAsync(command);
        }

        [Fact]
        public async Task throws_when_command_is_null()
        {
            var exception = await Record.ExceptionAsync(() => act(null));

            exception.ShouldBeOfType(typeof(AwesomeCalendarException));
            Assert.Equal(((AwesomeCalendarException)exception).Type, AwesomeCalendarExceptionType.NullCommand);
        }

        [Theory, AutoData]
        public async Task throws_when_command_has_empty_id(DeleteCalendarItemCommand command)
        {
            command.Id = Guid.Empty;

            var exception = await Record.ExceptionAsync(() => act(command));

            exception.ShouldBeOfType(typeof(AwesomeCalendarException));
            Assert.Equal(((AwesomeCalendarException)exception).Type, AwesomeCalendarExceptionType.InvalidCommand);
        }

        [Theory, AutoData]
        public async Task throws_when_aggregate_not_found(CreateCalendarItemCommand createCommand, DeleteCalendarItemCommand deleteCommand)
        {
            createCommand.EndDate = createCommand.StartDate.AddDays(1);
            await CreateCommandHandler.HandleAsync(createCommand);

            var exception = await Record.ExceptionAsync(() => act(deleteCommand));

            exception.ShouldBeOfType(typeof(AwesomeCalendarException));
        }
    }
}
