using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResposibility
{
    public interface IHandler
    {
        void setNext(IHandler handler);
        void handle(Request request);
        bool Nhandle(Request request);
    }
    public class Request
    {
        public int amount { get; set; }
        public string name { get; set; }
        public int xx { get; set; }
    }
    public class ManagerHandler : IHandler
    {
        private IHandler handler { get; set; }
        public void handle(Request request)
        {
            if (request.name =="zhangsan")
            {
                Console.WriteLine("张三干的很棒，直接通过");
            }
            if (request.amount < 500)
            {
                Console.WriteLine($"金额是{request.amount}元,500元以下直接通过干就行");
            }
            else
            {
                Console.WriteLine("管理权限不足交由区域经理审批");
                if(handle !=null)
                {
                    handler.handle(request);
                }
            }
        }

        public void setNext(IHandler handler)
        {
            this.handler = handler;
        }

        public bool Nhandle(Request request)
        {
            if (request.amount < 500)
            {
                Console.WriteLine($"金额是{request.amount}元,500元以下直接通过干就行");
                return true;
            }
            return false;
        }
    }
    public class DirectManagerHandler : IHandler
    {
        private IHandler handler { get; set;}

        public void handle(Request request)
        {
            if (request.amount < 1000)
            {
                Console.WriteLine($"金额是{request.amount}元,1000元以下区域经理我是一点问题都没有的,交给我审批");
            }
            else
            {
                Console.WriteLine("区域管理权限不足交由ceo直接审批");
                if (handler != null)
                {
                    handler.handle(request);
                }
            }
        }

        public void setNext(IHandler handler)
        {
           this.handler = handler;
        }

        public bool Nhandle(Request request)
        {
            if (request.amount < 1000)
            {
                Console.WriteLine($"金额是{request.amount}元,1000元以下区域经理我是一点问题都没有的,交给我审批");
                return true;
            }        
            return false;
        }
    }
    public class CeoHandler : IHandler
    {
        private IHandler handler { get; set; }

        public void handle(Request request)
        {
            Console.WriteLine($"金额是{request.amount}元，不管多少我ceo都是可以审批的");
        }

        public void setNext(IHandler handler)
        {
            this.handler = handler;
        }

        public bool Nhandle(Request request)
        {
            Console.WriteLine($"金额是{request.amount}元，不管多少我ceo都是可以审批的");
            return true;
        }
    }
}
