using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuckFramework.CustomAop
{
    public abstract class BaseInterceptorAttribute:Attribute
    {
        public abstract Action Do(Action action, IInvocation invocation);
        
    }
}
