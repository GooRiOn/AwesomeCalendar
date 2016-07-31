using System;
using System.Collections.Generic;
using AwesomeCalendar.Infrastructure.Enums;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.Contracts.Commands
{
    public class EditCalendarItemCommand : ICommand
    {
        public Guid Id { get; } = Guid.NewGuid();

        public string UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IEnumerable<CalendarItemCycle> Cycles { get; set; }

        public CalendarItemEditionType EditionType { get; set; }
    }
}