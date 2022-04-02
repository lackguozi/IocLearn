using System;
using System.Collections.Generic;
using System.Text;

namespace LuckFramework.CustomAop
{
    public class CommonClass
    {
        public virtual void MethodInterceptor()
        {
            Console.WriteLine("使用aop方法");
        }
        public void MethodNoInterceptor()
        {
            Console.WriteLine("不使用aop方法");
        }
    }
}
