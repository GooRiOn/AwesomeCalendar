using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.Infrastructure.Interfaces.Handlers
{
    public interface ICommandHandler<in TCommand> : IHandle<TCommand> where TCommand : class, ICommand
    {
        
    }
}