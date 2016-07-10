using System.Data.Entity;

namespace AwesomeCalendar.DataAccess
{
    public class EventStoreContext : DbContext
    {
        static EventStoreContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EventStoreContext>());
        }

        public EventStoreContext()
            :base(nameof(EventStoreContext))
        {
        }
    }
}
