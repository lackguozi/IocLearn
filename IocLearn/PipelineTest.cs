using LuckFramework;
using LuckFramework.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IocLearn
{
    public  class PipelineTest
    {
        private class RequestContext
        {
            public string RequestName { get; set; }
            public int Hour { get; set; }
        }
        public static void Test()
        {
            var requestContext = new RequestContext()
            {
                RequestName = "Lucktian",
                Hour = 12,
            };
            var builder = PipelineBuild.Create<RequestContext>(context =>
            {
                Console.WriteLine($"{context.RequestName} {context.Hour}h apply failed");
            })
               .Use((context, next) =>
               {
                   if (context.Hour <= 2)
                   {
                       Console.WriteLine("pass 1");
                   }
                   else
                   {
                       next();
                   }
               })
               .Use((context, next) =>
               {
                   if (context.Hour <= 4)
                   {
                       Console.WriteLine("pass 2");
                   }
                   else
                   {
                       next();
                   }
               })
               .Use((context, next) =>
               {
                   if (context.Hour <= 6)
                   {
                       Console.WriteLine("pass 3");
                   }
                   else
                   {
                       next();
                   }
               })
           ;
            var requestPipeline = builder.Build();
            Console.WriteLine();
            foreach (var i in Enumerable.Range(1, 8))
            {
                Console.WriteLine($"--------- h:{i} apply Pipeline------------------");
                requestContext.Hour = i;
                requestPipeline.Invoke(requestContext);
                Console.WriteLine("----------------------------");
            }

        }
    }
    
}
