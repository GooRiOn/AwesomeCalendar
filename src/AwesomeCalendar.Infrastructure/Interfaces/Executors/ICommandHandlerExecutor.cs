using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.Infrastructure.Interfaces.Executors
{
    public interface ICommandHandlerExecutor
    {
        void Execute<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}