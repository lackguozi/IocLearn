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
            return new PipelineBuilder<TContext>(completeAction);
        }
    }
}
