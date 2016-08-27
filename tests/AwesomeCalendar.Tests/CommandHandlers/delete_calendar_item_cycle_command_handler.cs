using System;
using System.Threading.Tasks;
using AwesomeCalendar.Contracts.Commands;
using AwesomeCalendar.DataAccess;
using AwesomeCalendar.DataAccess.Database;
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
    public class delete_calendar_item_cycle_command_handler
    {
        ICommandHandler<CreateCalendarItemCommand> CreateCommandHandler { get; }
        ICommandHandler<DeleteCalendarItemCycleCommand> DeleteCommandHandler { get; }
        IEventStore EventStore { get; }

        public delete_calendar_item_cycle_command_handler()
        {
            EventStore = new FakeEventStore();
            CreateCommandHandler = new CreateCalendarItemCommandHandler(EventStore);
            DeleteCommandHandler = new DeleteCalendarItemCycleCommandHandler(EventStore);
        }

        async Task act(DeleteCalendarItemCycleCommand command)
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
        public async Task throws_when_command_has_empty_id(DeleteCalendarItemCycleCommand command)
        {
            command.Id = Guid.Empty;

            var exception = await Record.ExceptionAsync(() => act(command));

            exception.ShouldBeOfType(typeof(AwesomeCalendarException));
            Assert.Equal(((AwesomeCalendarException)exception).Type, AwesomeCalendarExceptionType.InvalidCommand);
        }

        [Theory, AutoData]
        public async Task throws_when_command_aggregate_has_empty_id(DeleteCalendarItemCycleCommand command)
        {
            command.AggregateId = Guid.Empty;

            var exception = await Record.ExceptionAsync(() => act(command));

            exception.ShouldBeOfType(typeof(AwesomeCalendarException));
            Assert.Equal(((AwesomeCalendarException)exception).Type, AwesomeCalendarExceptionType.InvalidCommand);
        }
    }
}
