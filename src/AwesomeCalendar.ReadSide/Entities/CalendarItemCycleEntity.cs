using System;
using System.ComponentModel.DataAnnotations.Schema;
using AwesomeCalendar.Infrastructure.Enums;

namespace AwesomeCalendar.ReadSide.Entities
{
    public class CalendarItemCycleEntity : InternalEntity
    {
        public CalendarItemCycleEntity()
        {
            Id = Guid.NewGuid();
        }
        public Guid CalendarItemId { get; set; }

        [ForeignKey("CalendarItemId")]
        public CalendarItemEntity CalendarItem { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public CalendarItemCycleType Type { get; set; }

        public int Interval { get; set; }
    }
}