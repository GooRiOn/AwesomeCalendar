using AwesomeCalendar.Infrastructure.Interfaces.Contracts;
using AwesomeCalendar.Infrastructure.Interfaces.Handlers;

namespace AwesomeCalendar.Infrastructure.Interfaces.Factories
{
    public interface ICommandHandlerExecutor
    {
        void Execute<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}