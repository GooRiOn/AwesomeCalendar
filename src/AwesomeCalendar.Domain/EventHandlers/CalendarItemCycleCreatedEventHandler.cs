using System.Threading.Tasks;
using AwesomeCalendar.Contracts.Events;
using AwesomeCalendar.Infrastructure.Interfaces.Handlers;
using AwesomeCalendar.ReadSide.Entities;
using AwesomeCalendar.ReadSide.Repositories.Interfaces;

namespace AwesomeCalendar.Domain.EventHandlers
{
    public class CalendarItemCycleCreatedEventHandler : IEventHandler<CalendarItemCycleCreatedEvent>
    {
        ICalendarItemCycleRepository CalendarItemCycleRepository { get; }

        public CalendarItemCycleCreatedEventHandler(ICalendarItemCycleRepository calendarItemCycleRepository)
        {
            CalendarItemCycleRepository = calendarItemCycleRepository;
        }

        public async Task HandleAsync(CalendarItemCycleCreatedEvent @event) =>
            await CalendarItemCycleRepository.AddAsync(new CalendarItemCycleEntity
            {
                CalendarItemId = @event.AggregateId,
                Type = @event.Type,
                Interval = @event.Interval,
                StartDate = @event.StartDate,
                EndDate = @event.EndDate
            });
        
    }
}