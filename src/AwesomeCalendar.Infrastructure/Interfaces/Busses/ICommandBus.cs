using System.Threading.Tasks;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.Infrastructure.Interfaces.Busses
{
    public interface ICommandBus
    {
        void Send<TCommand>(TCommand command) where TCommand : class, ICommand;
        Task SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}