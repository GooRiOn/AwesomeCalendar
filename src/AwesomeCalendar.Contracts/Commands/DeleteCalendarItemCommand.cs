using System;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.Contracts.Commands
{
    public class DeleteCalendarItemCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
