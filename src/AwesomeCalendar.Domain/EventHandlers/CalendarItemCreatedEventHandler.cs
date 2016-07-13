using AwesomeCalendar.Contracts.Events;
using AwesomeCalendar.Infrastructure.Interfaces.Handlers;
using AwesomeCalendar.ReadSide.Entities;
using AwesomeCalendar.ReadSide.Repositories.Interfaces;

namespace AwesomeCalendar.Domain.EventHandlers
{
    public class CalendarItemCreatedEventHandler : IEventHandler<CalendarItemCreatedEvent>
    {
        ICalendarItemRepository CalendarItemRepository { get; }

        public CalendarItemCreatedEventHandler(ICalendarItemRepository calendarItemRepository)
        {
            CalendarItemRepository = calendarItemRepository;
        }

        public void Handle(CalendarItemCreatedEvent @event)
        {
            CalendarItemRepository.Add(new CalendarItemEntity
            {
                Id = @event.AggregateId,
                UserId = @event.UserId,
                Name = @event.Name,
                Description = @event.Description,
                StartDate = @event.StartDate,
                EndDate = @event.EndDate
            });
        }
    }
}
