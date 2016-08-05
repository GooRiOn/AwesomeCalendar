using System.Threading.Tasks;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.Infrastructure.Interfaces.Busses
{
    public interface IEventBus
    {
        Task SendAsync<TEvent>(TEvent @event) where TEvent : class, IEvent;
    }
}