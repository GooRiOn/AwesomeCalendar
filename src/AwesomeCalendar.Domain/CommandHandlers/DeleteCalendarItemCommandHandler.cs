using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwesomeCalendar.Contracts.Commands;
using AwesomeCalendar.Infrastructure.Interfaces.DataAccess;
using AwesomeCalendar.Infrastructure.Interfaces.Handlers;

namespace AwesomeCalendar.Domain.CommandHandlers
{
    public class DeleteCalendarItemCommandHandler : ICommandHandler<DeleteCalendarItemCommand>
    {
        private IEventStore EventSotre { get; }

        public DeleteCalendarItemCommandHandler(IEventStore eventStore)
        {
            EventSotre = eventStore;
        }

        public void Handle(DeleteCalendarItemCommand command)
        {
            throw new NotImplementedException();
        }

        public void Validate(DeleteCalendarItemCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
