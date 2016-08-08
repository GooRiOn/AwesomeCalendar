using AwesomeCalendar.Infrastructure.Interfaces.Contracts;
using AwesomeCalendar.Infrastructure.Interfaces.HandleResult;

namespace AwesomeCalendar.Infrastructure.Interfaces.Busses
{
    public interface ICommandBusExecutor
    {
        IHandleResult Execute(ICommand command);
    }
}