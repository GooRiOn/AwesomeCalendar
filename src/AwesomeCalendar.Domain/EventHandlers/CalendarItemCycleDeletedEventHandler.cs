using System;
using System.Threading.Tasks;
using AwesomeCalendar.Contracts.Events;
using AwesomeCalendar.Infrastructure.Interfaces.Handlers;
using AwesomeCalendar.ReadSide.Repositories.Interfaces;

namespace AwesomeCalendar.Domain.EventHandlers
{
    public class CalendarItemCycleDeletedEventHandler : IEventHandler<CalendarItemCycleDeletedEvent>
    {
        ICalendarItemRepository CalendarItemRepository { get; }

        public CalendarItemCycleDeletedEventHandler(ICalendarItemRepository calendarItemRepository)
        {
            CalendarItemRepository = calendarItemRepository;
        }

        public Task HandleAsync(CalendarItemCycleDeletedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
