using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResposibility
{
    public class HandlerChain
    {
        private List<IHandler> handlers = new List<IHandler>();
        public HandlerChain AddHandler(IHandler handler)
        {
            handlers.Add(handler);
            return this;
        }

        public void Handle(Request request)
        {
            foreach (IHandler handler in handlers)
            {
                if (handler != null)
                {
                    bool flag = handler.Nhandle(request);
                    if (flag)
                    {
                        Console.WriteLine($"request 成功处理，处理器是{handler.GetType().FullName}");
                        return;
                    }
                }
            }
        }
    }
}
