using System;
using System.Linq;
using System.Linq.Expressions;
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
    public class create_calendar_item_command_handler_tests
    {
        ICommandHandler<CreateCalendarItemCommand> CommandHandler { get; }
        IEventStore EventStore { get; }

        public create_calendar_item_command_handler_tests()
        {
            EventStore = new FakeEventStore();
            CommandHandler = new CreateCalendarItemCommandHandler(EventStore);
        }

        async Task act(CreateCalendarItemCommand command)
        {
            await CommandHandler.HandleAsync(command);
        }

        [Theory, AutoData]
        public async Task persists_event_to_event_store_needed_to_reconstruct_calendar_item(CreateCalendarItemCommand command)
        {
            command.StartDate = DateTime.UtcNow;
            command.EndDate = DateTime.UtcNow.AddHours(1);

            await act(command);
            var createdAggreagte = await EventStore.GetByIdAsync<CalendarItem, CalendarItemBaseEvent>(command.Id);

            createdAggreagte.ShouldBeOfType(typeof(CalendarItem));
            Assert.Equal(command.Id, createdAggreagte.Id); 
        }

        [Fact]
        public async Task throws_when_command_is_null()
        {
            var exception = await Record.ExceptionAsync(async ()=> await act(null));

            exception.ShouldBeOfType(typeof(AwesomeCalendarException));
            Assert.Equal(((AwesomeCalendarException)exception).Type,AwesomeCalendarExceptionType.NullCommand);
        }       

        [Theory]
        [InlineData("","name", "2016-12-12", "2016-12-23")]
        [InlineData("userId", "", "2016-12-12", "2016-12-23")]
        [InlineData("userId", "name", "2016-12-12", "2016-12-11")]
        public async Task throws_when_command_has_invalid_data(string userId, string name, DateTime startDate, DateTime endDate)
        {
            var command = new CreateCalendarItemCommand
            {
                UserId = userId,
                Name = name,
                StartDate = startDate,
                EndDate = endDate
            };

            var exception = await Record.ExceptionAsync(async () => await act(command));

            exception.ShouldBeOfType(typeof(AwesomeCalendarException));
            Assert.Equal(((AwesomeCalendarException)exception).Type, AwesomeCalendarExceptionType.InvalidCommand);
        }
    }
}