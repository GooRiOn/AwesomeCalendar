using AwesomeCalendar.ReadSide.Entities;
using AwesomeCalendar.ReadSide.Repositories.Interfaces;

namespace AwesomeCalendar.ReadSide.Repositories
{
    public sealed class CalendarItemRepository : Repository<CalendarItemEntity>, ICalendarItemRepository
    {
        public CalendarItemRepository(ReadSideContext readSideContext) : base(readSideContext)
        {
        }
    }
}