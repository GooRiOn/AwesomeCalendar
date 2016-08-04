using AwesomeCalendar.Infrastructure.Interfaces.HandleResult;

namespace AwesomeCalendar.Messaging.HandlingResult
{
    public class HandleResult : IHandleResult
    {
        public bool Succeed { get; }

        public HandleResult(bool succeed)
        {
            Succeed = succeed;
        }
    }
}