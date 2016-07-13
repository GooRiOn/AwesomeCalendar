using System;
using System.Threading;
using AwesomeCalendar.Infrastructure.Enums;

namespace AwesomeCalendar.Infrastructure.Exceptions
{
    public class AwesomeCalendarException : Exception
    {
        AwesomeCalendarExceptionType Type { get; }
        Type SourceType { get; }
        string Message { get; set; }

        public AwesomeCalendarException(AwesomeCalendarExceptionType type, Type sourceType ,string message = null)
        {
            Type = type;
            SourceType = sourceType;
            Message = message;
        }
    }
}