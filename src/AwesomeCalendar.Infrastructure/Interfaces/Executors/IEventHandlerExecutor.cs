using System.Threading.Tasks;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.Infrastructure.Interfaces.Executors
{
    public interface IEventHandlerExecutor
    {
        Task ExecuteAsync<TEvent>(TEvent @event) where TEvent : class, IEvent;
    }
}