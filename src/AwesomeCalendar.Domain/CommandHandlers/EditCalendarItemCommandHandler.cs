﻿using System;
using System.Linq;
using AwesomeCalendar.Contracts.Commands;
using AwesomeCalendar.Contracts.Events;
using AwesomeCalendar.Domain.Aggregates;
using AwesomeCalendar.Infrastructure.Enums;
using AwesomeCalendar.Infrastructure.Exceptions;
using AwesomeCalendar.Infrastructure.Interfaces.DataAccess;
using AwesomeCalendar.Infrastructure.Interfaces.Handlers;

namespace AwesomeCalendar.Domain.CommandHandlers
{
    public class EditCalendarItemCommandHandler : ICommandHandler<EditCalendarItemCommand>
    {
        IEventStore EventStore { get; set; }

        public EditCalendarItemCommandHandler(IEventStore eventStore)
        {
            EventStore = eventStore;
        }

        public void Handle(EditCalendarItemCommand command)
        {
            var calendarItem = EventStore.GetById<CalendarItem, CalendarItemBaseEvent>(command.Id);

            calendarItem.Edit(
                command.UserId,
                command.Name,
                command.Description,
                command.EditionType,
                command.StartDate,
                command.EndDate,
                command.Cycles.ToList());

            EventStore.Persist(calendarItem);
        }

        public void Validate(EditCalendarItemCommand command)
        {
            if( command == null)
                throw new AwesomeCalendarException(AwesomeCalendarExceptionType.NullCommand, typeof(EditCalendarItemCommand));

            if(command.Id == Guid.Empty || string.IsNullOrEmpty(command.UserId) || string.IsNullOrEmpty(command.Name) || command.StartDate > command.EndDate)
                throw new AwesomeCalendarException(AwesomeCalendarExceptionType.InvalidCommand, typeof(EditCalendarItemCommand));
        }
    }
}