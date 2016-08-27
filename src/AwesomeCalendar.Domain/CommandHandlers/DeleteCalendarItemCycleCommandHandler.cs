using System;
using System.Threading.Tasks;
using AwesomeCalendar.Contracts.Commands;
using AwesomeCalendar.Domain.Aggregates;
using AwesomeCalendar.Infrastructure.Enums;
using AwesomeCalendar.Infrastructure.Exceptions;
using AwesomeCalendar.Infrastructure.Interfaces.DataAccess;
using AwesomeCalendar.Infrastructure.Interfaces.Handlers;

namespace AwesomeCalendar.Domain.CommandHandlers
{
    public class DeleteCalendarItemCycleCommandHandler : ICommandHandler<DeleteCalendarItemCycleCommand>
    {
        private IEventStore EventStore { get; }

        public DeleteCalendarItemCycleCommandHandler(IEventStore eventStore)
        {
            EventStore = eventStore;
        }

        public async Task HandleAsync(DeleteCalendarItemCycleCommand command)
        {
            ((ICommandHandler<DeleteCalendarItemCycleCommand>) this).Validate(command);

            var calendarItem = await EventStore.GetByIdAsync<CalendarItem>(command.AggregateId);

            calendarItem.DeleteCycle(command.Id);

            await EventStore.PersistAsync(calendarItem);
        }

        void ICommandHandler<DeleteCalendarItemCycleCommand>.Validate(DeleteCalendarItemCycleCommand command)
        {
            if(command == null)
                throw new AwesomeCalendarException(AwesomeCalendarExceptionType.NullCommand, typeof(DeleteCalendarItemCycleCommand));

            if(command.Id == Guid.Empty || command.AggregateId == Guid.Empty)
                throw new AwesomeCalendarException(AwesomeCalendarExceptionType.InvalidCommand, typeof(DeleteCalendarItemCycleCommand))
        }
    }
}
