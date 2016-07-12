using System;
using AwesomeCalendar.Contracts.Commands;
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
            throw new NotImplementedException();
        }

        public void Validate(CreateCalendarItemCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
