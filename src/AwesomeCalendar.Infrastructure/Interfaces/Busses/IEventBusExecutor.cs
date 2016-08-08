using System.Threading.Tasks;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.Infrastructure.Interfaces.Busses
{
    public interface IEventBusExecutor
    {
        void ExecuteAsync(IEvent @event);
    }
}