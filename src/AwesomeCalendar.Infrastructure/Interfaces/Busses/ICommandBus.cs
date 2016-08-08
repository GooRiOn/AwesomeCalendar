using System.Threading.Tasks;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;
using AwesomeCalendar.Infrastructure.Interfaces.HandleResult;

namespace AwesomeCalendar.Infrastructure.Interfaces.Busses
{
    public interface ICommandBus
    {
        Task<IHandleResult> SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}