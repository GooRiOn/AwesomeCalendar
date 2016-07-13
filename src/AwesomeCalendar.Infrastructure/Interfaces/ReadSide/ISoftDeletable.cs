namespace AwesomeCalendar.Infrastructure.Interfaces.ReadSide
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; }

        void SoftDelete();
    }
}