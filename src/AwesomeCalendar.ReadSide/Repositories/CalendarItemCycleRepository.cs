using AwesomeCalendar.ReadSide.Entities;
using AwesomeCalendar.ReadSide.Repositories.Interfaces;

namespace AwesomeCalendar.ReadSide.Repositories
{
    public sealed class CalendarItemCycleRepository : Repository<CalendarItemCycleEntity>, ICalendarItemCycleRepository
    {
        public CalendarItemCycleRepository(ReadSideContext readSideContext) : base(readSideContext)
        {
        }
    }
}