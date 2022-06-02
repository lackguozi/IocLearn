using System;
using System.Collections.Generic;
using System.Text;

namespace LuckFramework.Pipeline
{
    public static class PipelineBuilderExtensions
    {
        public static IPipelineBuilder<TContext> Use<TContext>(this IPipelineBuilder<TContext> builder, Action<TContext, Action> action)

        {
            return builder.Use(next =>
            {
                return (context) =>
                {
                    action(context, () => next(context));
                };
            });
               
        }

        public static IPipelineBuilder<TContext> Use<TContext>(this IPipelineBuilder<TContext> builder, Action<TContext, Action<TContext>> action)
        {
            return builder.Use(next =>
            {
                return context =>
                {
                    action(context, next);
                };
            });
               
        }

        public static IPipelineBuilder<TContext> Run<TContext>(this IPipelineBuilder<TContext> builder, Action<TContext> handler)
        {
            return builder.Use(_ => handler);
        }
        public static IPipelineBuilder<TContext> When<TContext>(this IPipelineBuilder<TContext> builder, Func<TContext, bool> predict, Action<IPipelineBuilder<TContext>> configureAction)

        {
            return builder.Use((context, next) =>
            {
                if (predict.Invoke(context))
                {
                    var branchPipelineBuilder = builder.New();
                    configureAction(branchPipelineBuilder);
                    var branchPipeline = branchPipelineBuilder.Build();
                    branchPipeline.Invoke(context);
                }
                else
                {
                    next();
                }
            });
        }
    }
}
