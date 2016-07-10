using System;
using System.Threading.Tasks;
using AwesomeCalendar.Infrastructure.Interfaces.Busses;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;
using AwesomeCalendar.Infrastructure.Interfaces.Executors;
using EasyNetQ;

namespace AwesomeCalendar.Messaging.Busses
{
    public class CommandBus : ICommandBus
    {
        IBus Bus { get; }
        ICommandHandlerExecutor CommandHandlerFactory { get; }

        public CommandBus(ICommandHandlerExecutor commandHandlerFactory)
        {
            CommandHandlerFactory = commandHandlerFactory;

            Bus = RabbitHutch.CreateBus("host=localhost");
            Bus.Receive(nameof(CommandBus), (Action<ICommand>) ProccessBus);
        }

        public void Send<TCommand>(TCommand command) where TCommand : class, ICommand =>
            Bus.Publish(command);

        public async Task SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand =>
            await Bus.PublishAsync(command);

        void ProccessBus(ICommand command)
        {
            var commandType = command.GetType();
            var factoryType = CommandHandlerFactory.GetType();

            factoryType.GetMethod(nameof(ICommandHandlerExecutor.Execute))
                .MakeGenericMethod(commandType)
                .Invoke(CommandHandlerFactory, new[] {command});
        }
    }
}
