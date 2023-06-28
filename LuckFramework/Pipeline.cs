using LuckFramework.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuckFramework
{
    public static class PipelineBuild
    {
        public static IPipelineBuilder<TContext> Create<TContext>(Action<TContext> completeAction)
        {
            Console.WriteLine("test dev");
            return new PipelineBuilder<TContext>(completeAction);
        }
    }
}
