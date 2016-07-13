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
        ICommandHandlerExecutor CommandHandlerExecutor { get; }

        public CommandBus(ICommandHandlerExecutor commandHandlerExecutor)
        {
            CommandHandlerExecutor = commandHandlerExecutor;

            Bus = RabbitHutch.CreateBus("host=localhost");
            Bus.Receive(nameof(CommandBus), (Action<ICommand>) ProccessBus);
        }

        public void Send<TCommand>(TCommand command) where TCommand : class, ICommand =>
            Bus.Send(nameof(CommandBus),command);


        public async Task SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand =>
            await Bus.SendAsync(nameof(CommandBus), command);


        void ProccessBus(ICommand command) 
        {
            Bus.Respond<ICommand, string>(responder => "test");

            var commandType = command.GetType();
            var executorType = CommandHandlerExecutor.GetType();

            executorType.GetMethod(nameof(ICommandHandlerExecutor.Execute))
                .MakeGenericMethod(commandType)
                .Invoke(CommandHandlerExecutor, new[] { command });
        }
    }
}
