using AwesomeCalendar.Infrastructure.DependencyInjection.Interfaces;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;
using AwesomeCalendar.Infrastructure.Interfaces.Factories;
using AwesomeCalendar.Infrastructure.Interfaces.Handlers;

namespace AwesomeCalendar.Domain.Factories
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        ICustomDependencyResolver CustomDependencyResolver { get; }

        public CommandHandlerFactory(ICustomDependencyResolver customDependencyResolver)
        {
            CustomDependencyResolver = customDependencyResolver;
        }

        public ICommandHandler<TCommand> Get<TCommand>() where TCommand : class, ICommand 
            => CustomDependencyResolver.Resolve<ICommandHandler<TCommand>>();
    }
}
