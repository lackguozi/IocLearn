namespace ChainOfResposibility
{
    internal class Program
    {
        static void Main(string[] args)
        {

             Request request = new Request()
             {
                 name = "张三",
                 amount = 800
             };
            //好处是写的很清楚，逻辑比较明朗，但是写的很不优雅，而且处理器还要负责下一个处理器的引用，责任不明确.
            #region 传统写法

            ManagerHandler manegerHandler = new ManagerHandler();
            DirectManagerHandler directManagerHandler = new DirectManagerHandler();
            manegerHandler.setNext(directManagerHandler);
            directManagerHandler.setNext(new CeoHandler());

            manegerHandler.handle(request);
            #endregion

            #region


            HandlerChain handlerChain = new HandlerChain();
            handlerChain.AddHandler(new ManagerHandler()).AddHandler(new DirectManagerHandler()).AddHandler(new CeoHandler());
            handlerChain.Handle(request);
            #endregion

        }
    }
}