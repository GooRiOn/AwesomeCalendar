using System.Threading.Tasks;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.Infrastructure.Interfaces.Executors
{
    public interface ICommandHandlerExecutor
    {
        Task ExecuteAsync<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}