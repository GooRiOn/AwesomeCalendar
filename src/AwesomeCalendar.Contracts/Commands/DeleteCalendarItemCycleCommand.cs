using System;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.Contracts.Commands
{
    public class DeleteCalendarItemCycleCommand : ICommand
    {
        public Guid Id { get; set; }

        public Guid AggregateId { get; set; }
    }
}
