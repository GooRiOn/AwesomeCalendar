using System.Threading.Tasks;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.Infrastructure.Interfaces.Busses
{
    public interface IEventBus
    {
        void Send<TEvent>(TEvent @event) where TEvent : class, IEvent;
        Task SendAsync<TEvent>(TEvent @event) where TEvent : class, IEvent;
    }
}