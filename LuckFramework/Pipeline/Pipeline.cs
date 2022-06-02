using System;
using System.Collections.Generic;
using System.Text;

namespace LuckFramework.Pipeline
{
    public interface IPipelineBuilder<TContext>: IProperties
    {
        IPipelineBuilder<TContext> Use(Func<Action<TContext>, Action<TContext>> middleware);
        Action<TContext> Build();
        IPipelineBuilder<TContext> New ();
    }
    public class PipelineBuilder<TContext> : IPipelineBuilder<TContext>
    {
        private readonly Action<TContext> _completeFunc;
        private readonly List<Func<Action<TContext>, Action<TContext>>> _pipelines = new List<Func<Action<TContext>, Action<TContext>>>();

        public PipelineBuilder(Action<TContext> completeFunc)
        {
            _completeFunc = completeFunc;
        }

        public IDictionary<string, object> Properties { get; } = new Dictionary<string,Object>();

        public Action<TContext> Build()
        {
            var request = _completeFunc;
            for (int i = _pipelines.Count-1;i>=0; i--)
            {
                request = _pipelines[i](request);
            }
            return request;
        }

        public IPipelineBuilder<TContext> New()=> new PipelineBuilder<TContext>(_completeFunc);
        /* {
             return new PipelineBuilder<TContext>(_completeFunc);
         }*/

        public IPipelineBuilder<TContext> Use(Func<Action<TContext>, Action<TContext>> middleware)
        {
           _pipelines.Add(middleware);
            return this;
        }
    }
    public interface IProperties
    {
        IDictionary<string, object?> Properties { get; }
    }
}
