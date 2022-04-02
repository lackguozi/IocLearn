using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LuckFramework.CustomAop
{
    public static class LuckContainerAopExtend
    {
        public static object CreateProxtAOP(this object t,Type interfacetype)
        {
            ProxyGenerator generator = new ProxyGenerator();
            IOcInterceptor customInterceptor = new IOcInterceptor();
            t = generator.CreateInterfaceProxyWithTarget(interfacetype, t, customInterceptor);
            return t;
        }
    }
    #region attribute  interceptor
    public class LogBrforeAttribute : BaseInterceptorAttribute 
    {
        

        public override Action Do(Action action, IInvocation invocation)
        {
            return () =>
            {
                Console.WriteLine("方法执行之前");
                action.Invoke();
            };

        }
    }
    public class LogAfterAttribute : BaseInterceptorAttribute
    {
        public override Action Do(Action action, IInvocation invocation)
        {
            return () =>
            {
                
                action.Invoke();
                Console.WriteLine("方法执行 Hou");
            };
        }
    }

    #endregion
    public class IOcInterceptor : StandardInterceptor
    {
        /// <summary>
        /// 执行前
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PreProceed(IInvocation invocation)
        {
            Console.WriteLine(invocation.Method.Name + "执行前,入参：" + string.Join(",", invocation.Arguments));
        }

        /// <summary>
        /// 执行中
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PerformProceed(IInvocation invocation)
        {
            Console.WriteLine(invocation.Method.Name + "执行中");
            var method = invocation.Method;
            Action action =() => base.PerformProceed(invocation);
            if (method.IsDefined(typeof(BaseInterceptorAttribute), true))
            {
                foreach(var attribute in method.GetCustomAttributes<BaseInterceptorAttribute>())
                {
                    action = attribute.Do(action, invocation);
                }
                
            }
            action.Invoke();
            
            
        }

        /// <summary>
        /// 执行后
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PostProceed(IInvocation invocation)
        {
            Console.WriteLine(invocation.Method.Name + "执行后，返回值：" + invocation.ReturnValue);
        }
    }
}
