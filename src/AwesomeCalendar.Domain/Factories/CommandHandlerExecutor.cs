using AwesomeCalendar.Infrastructure.DependencyInjection.Interfaces;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;
using AwesomeCalendar.Infrastructure.Interfaces.Executors;
using AwesomeCalendar.Infrastructure.Interfaces.Handlers;

namespace AwesomeCalendar.Domain.Factories
{
    public class CommandHandlerExecutor : ICommandHandlerExecutor
    {
        ICustomDependencyResolver CustomDependencyResolver { get; }

        public CommandHandlerExecutor(ICustomDependencyResolver customDependencyResolver)
        {
            CustomDependencyResolver = customDependencyResolver;
        }

        public void Execute<TCommand>(TCommand command) where TCommand : class, ICommand
            => CustomDependencyResolver.Resolve<ICommandHandler<TCommand>>()
                .Handle(command);
    }
}
