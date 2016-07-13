using System;

namespace AwesomeCalendar.Infrastructure.Interfaces.ReadSide
{
    public interface IInternalEntity
    {
        Guid Id { get; set; }
    }
}