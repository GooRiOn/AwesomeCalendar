using System.Threading.Tasks;
using AwesomeCalendar.Contracts.Events;
using AwesomeCalendar.Infrastructure.Interfaces.Handlers;
using AwesomeCalendar.ReadSide.Repositories.Interfaces;

namespace AwesomeCalendar.Domain.EventHandlers
{
    class CalendarItemDeletedEventHandler : IEventHandler<CalendarItemDeletedEvent>
    {
        ICalendarItemRepository CalendarItemRepository { get; }

        public CalendarItemDeletedEventHandler(ICalendarItemRepository calendarItemRepository)
        {
            CalendarItemRepository = calendarItemRepository;
        }
       
        public async Task HandleAsync(CalendarItemDeletedEvent @event) =>
               await CalendarItemRepository.SoftDeleteAsync(@event.AggregateId);
    }
}
