using System;
using System.Collections.Generic;

namespace AwesomeCalendar.ReadSide.Entities
{
    public class CalendarItemEntity : InternalEntity
    {
        public CalendarItemEntity()
        {
            Cycles = new HashSet<CalendarItemCycleEntity>();
        }

        public CalendarItemEntity(Guid id) :this()
        {
            Id = id;
        }

        public string UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ICollection<CalendarItemCycleEntity> Cycles { get; set; }
    }
}