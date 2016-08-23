using System;
using System.Threading.Tasks;
using AwesomeCalendar.Contracts.Commands;
using AwesomeCalendar.Contracts.Events;
using AwesomeCalendar.Domain.Aggregates;
using AwesomeCalendar.Infrastructure.Enums;
using AwesomeCalendar.Infrastructure.Exceptions;
using AwesomeCalendar.Infrastructure.Interfaces.DataAccess;
using AwesomeCalendar.Infrastructure.Interfaces.Handlers;

namespace AwesomeCalendar.Domain.CommandHandlers
{
    public class DeleteCalendarItemCommandHandler : ICommandHandler<DeleteCalendarItemCommand>
    {
        IEventStore EventStore { get; }

        public DeleteCalendarItemCommandHandler(IEventStore eventStore)
        {
            EventStore = eventStore;
        }

        public async Task HandleAsync(DeleteCalendarItemCommand command)
        {
            ((ICommandHandler<DeleteCalendarItemCommand>) this).Validate(command);

            var calendarItem = await EventStore.GetByIdAsync<CalendarItem>(command.Id);

            calendarItem.Delete();

            await EventStore.PersistAsync(calendarItem);
        }

        void ICommandHandler<DeleteCalendarItemCommand>.Validate(DeleteCalendarItemCommand command)
        {
            if(command == null)
                throw new AwesomeCalendarException(AwesomeCalendarExceptionType.NullCommand, typeof(DeleteCalendarItemCommand));

            if(command.Id == Guid.Empty)
                throw new AwesomeCalendarException(AwesomeCalendarExceptionType.InvalidCommand, typeof(DeleteCalendarItemCommand));
        }
    }
}
