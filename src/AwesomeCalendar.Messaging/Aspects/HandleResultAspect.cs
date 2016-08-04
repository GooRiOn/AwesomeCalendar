using System;
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
            catch (Exception exception)
            {

                invocation.ReturnValue = new HandleResult(false);
            }
        }
    }
}