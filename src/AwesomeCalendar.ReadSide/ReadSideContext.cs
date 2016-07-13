using System.Data.Entity;
using AwesomeCalendar.ReadSide.Entities;

namespace AwesomeCalendar.ReadSide
{
    public class ReadSideContext : DbContext
    {
        static ReadSideContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ReadSideContext>());
        }

        public ReadSideContext() :base(nameof(ReadSideContext))
        {
            
        }

        public DbSet<CalendarItemEntity> CalendarItems { get; set; }

        public DbSet<CalendarItemCycleEntity> CalendarItemCycles { get; set; }
    }
}