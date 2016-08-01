namespace AwesomeCalendar.Infrastructure.Enums
{
    public enum AwesomeCalendarExceptionType
    {
        NullCommand,
        NullEvent,
        InvalidCommand,
        InvalidEvent,
        EventStoreConcurency,
        AggregateNotFound,
        AggregateDeleted
    }
}