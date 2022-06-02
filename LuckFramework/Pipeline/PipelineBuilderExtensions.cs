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
    }
}
