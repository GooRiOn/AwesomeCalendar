using System.Threading.Tasks;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.Infrastructure.Interfaces.Busses
{
    public interface IEventBusExecutor
    {
        Task ExecuteAsync(IEvent @event);
    }
}