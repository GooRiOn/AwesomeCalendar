using System.Threading.Tasks;
using AwesomeCalendar.Infrastructure.Interfaces.Busses;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;
using AwesomeCalendar.Infrastructure.Interfaces.Executors;
using AwesomeCalendar.Infrastructure.Interfaces.HandleResult;
using AwesomeCalendar.Messaging.HandlingResult;

namespace AwesomeCalendar.Messaging.Executors
{
    public class CommandBusExecutor : ICommandBusExecutor
    {
        ICommandHandlerExecutor CommandHandlerExecutor { get; }

        public CommandBusExecutor(ICommandHandlerExecutor commandHandlerExecutor)
        {
            CommandHandlerExecutor = commandHandlerExecutor;
        }

        public async Task<IHandleResult> ExecuteAsync(ICommand command)
        {
            var commandType = command.GetType();
            var executorType = CommandHandlerExecutor.GetType();

            await (Task) executorType.GetMethod(nameof(ICommandHandlerExecutor.ExecuteAsync))
                .MakeGenericMethod(commandType)
                .Invoke(CommandHandlerExecutor, new[] { command });

            return new HandleResult(true);
        }
    }
}