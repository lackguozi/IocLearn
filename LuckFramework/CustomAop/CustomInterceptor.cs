using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuckFramework.CustomAop
{
    public class CustomInterceptor:StandardInterceptor
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
            try
            {
                base.PerformProceed(invocation);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
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
