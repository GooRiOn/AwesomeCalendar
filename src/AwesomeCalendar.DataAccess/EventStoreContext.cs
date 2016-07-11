using System.Data.Entity;
using AwesomeCalendar.Contracts.Events;

namespace AwesomeCalendar.DataAccess
{
    public class EventStoreContext : DbContext
    {
        public DbSet<CalendarItemBaseEvent> CalendarItemEvents { get; set; }

        public DbSet<CalendarItemCreatedEvent> CalendarItemCreatedEvents { get; set; }

        public DbSet<CalendarItemCycleCreatedEvent> CalendarItemCycleCreatedEvents { get; set; }

        static EventStoreContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EventStoreContext>());
        }

        public EventStoreContext()
            :base(nameof(EventStoreContext))
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CalendarItemBaseEvent>().HasKey(e => e.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}
