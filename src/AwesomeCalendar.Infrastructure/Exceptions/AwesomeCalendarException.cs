using System;
using System.Threading;
using AwesomeCalendar.Infrastructure.Enums;

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