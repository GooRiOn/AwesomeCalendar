namespace AwesomeCalendar.Infrastructure.Interfaces.Contracts
{
    public interface IHandle<in TCommand> where TCommand : class, ICommand
    {
        void Handle(TCommand command);
    }
}