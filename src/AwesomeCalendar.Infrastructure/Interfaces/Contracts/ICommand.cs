using System;

namespace AwesomeCalendar.Infrastructure.Interfaces.Contracts
{
    public interface ICommand
    {
        Guid Id { get; set; }
    }
}