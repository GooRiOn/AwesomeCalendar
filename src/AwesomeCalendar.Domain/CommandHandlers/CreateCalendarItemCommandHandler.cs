using System;
using AwesomeCalendar.Contracts.Commands;
using AwesomeCalendar.Domain.Aggregates;
using AwesomeCalendar.Infrastructure.Enums;
using AwesomeCalendar.Infrastructure.Exceptions;
using AwesomeCalendar.Infrastructure.Interfaces.DataAccess;
using AwesomeCalendar.Infrastructure.Interfaces.Handlers;

namespace AwesomeCalendar.Domain.Handlers
{
    public class CreateCalendarItemCommandHandler : ICommandHandler<CreateCalendarItemCommand>
    {
        IEventStore EventStore { get; }

        public CreateCalendarItemCommandHandler(IEventStore eventStore)
        {
            EventStore = eventStore;
        }

        public void Handle(CreateCalendarItemCommand command)
        {
            ((ICommandHandler<CreateCalendarItemCommand>) this).Validate(command);

            var calendarItem = new CalendarItem(command.Id, command.UserId, command.Name,command.Description,command.StartDate,command.EndDate,command.Cycles);
            EventStore.Persist(calendarItem);
        }

        void ICommandHandler<CreateCalendarItemCommand>.Validate(CreateCalendarItemCommand command)
        {
            if(command == null)
                throw new AwesomeCalendarException(AwesomeCalendarExceptionType.NullCommand, typeof(CreateCalendarItemCommand));

            if(command.Id == Guid.Empty || string.IsNullOrEmpty(command.UserId) || string.IsNullOrEmpty(command.Name) || command.StartDate > command.EndDate)
                throw new AwesomeCalendarException(AwesomeCalendarExceptionType.InvalidCommand, typeof(CreateCalendarItemCommand));
        }
    }
}
