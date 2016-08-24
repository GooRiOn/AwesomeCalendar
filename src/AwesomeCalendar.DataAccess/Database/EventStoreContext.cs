using System.Data.Entity;

namespace AwesomeCalendar.DataAccess.Database
{
    public class EventStoreContext : DbContext
    {
        public DbSet<EventStoreEntity> Events { get; set; }

        static EventStoreContext()
        {
            System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EventStoreContext>());
        }

        public EventStoreContext()
            :base(nameof(EventStoreContext))
        {
        }
    }
}
