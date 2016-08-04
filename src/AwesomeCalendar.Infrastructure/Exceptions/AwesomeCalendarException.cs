using System;
using System.Threading.Tasks;
using AwesomeCalendar.Infrastructure.Enums;
using AwesomeCalendar.Infrastructure.Interfaces.Contracts;

namespace AwesomeCalendar.Infrastructure.Exceptions
{
    public class AwesomeCalendarException : Exception
    {
        public AwesomeCalendarExceptionType Type { get; }
        public Type SourceType { get; }
        public string Message { get; set; }

        public AwesomeCalendarException(AwesomeCalendarExceptionType type, Type sourceType ,string message = null)
        {
            Type = type;
            SourceType = sourceType;
            Message = message;
        }
    }
}