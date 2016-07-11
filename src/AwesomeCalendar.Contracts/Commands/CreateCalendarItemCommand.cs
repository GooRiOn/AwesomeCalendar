using System;
using System.Collections.Generic;
using AwesomeCalendar.Infrastructure.Enums;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.Contracts.Commands
{
    public class CreateCalendarItemCommand : ICommand
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IEnumerable<CalendarItemCycle> Cycles { get; set; }
    }

    public class CalendarItemCycle
    {
        public Guid Id { get; set; }

        public DateTime? EndTime { get; set; }

        public CalendarItemCycleType Type { get; set; }

        public int Interval { get; set; }
    }
}