using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuckFramework.CustomAop
{
    public class CustomAOPTest
    {
        public static void show()
        {
            ProxyGenerator generator = new ProxyGenerator();
            CustomInterceptor customInterceptor =new CustomInterceptor();
            CommonClass test = generator.CreateClassProxy<CommonClass>(customInterceptor);

            Console.WriteLine("当前类型：{0}，父类型：{1}",test.GetType(),test.GetType().BaseType);
            Console.WriteLine("");
            test.MethodInterceptor();
            Console.WriteLine("");
            test.MethodNoInterceptor();
            Console.WriteLine();
            Console.ReadLine();

        }
        
        





    }
}
