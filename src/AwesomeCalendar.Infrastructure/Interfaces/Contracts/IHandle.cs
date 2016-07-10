namespace AwesomeCalendar.Infrastructure.Interfaces.Contracts
{
    public interface IHandle<in TCommand> where TCommand : class, IEvent
    {
        void Handle(TCommand command);
    }
}