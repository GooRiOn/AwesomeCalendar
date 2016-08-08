using System;
using AwesomeCalendar.Infrastructure.Exceptions;
using AwesomeCalendar.Messaging.HandlingResult;
using Castle.DynamicProxy;

namespace AwesomeCalendar.Messaging.Aspects
{
    public class HandleResultAspect : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception exception) when((exception.InnerException as AwesomeCalendarException) != null)
            {

                invocation.ReturnValue = new HandleResult(false);
            }
        }
    }
}