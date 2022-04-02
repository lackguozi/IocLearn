using IocLearn.BLL;
using IocLearn.DAL;
using IocLearn.Interface;
using IocLearn.Services;
using LuckFramework;
using LuckFramework.CustomAop;
using System;


namespace IocLearn
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region ioc部分

            /*LuckContainer container = new LuckContainer();
            container.Register<IUserBLL,UserBLL>();
            container.Register<IUserDAL, UserDAL>();
            container.Register<IUserService, UserService>();
            container.Register<IUserDAL, UserDALMysql>("mysql", lifetimeType: LifetimeType.Singleton);
            IUserBLL bll = container.ReSolve<IUserBLL>();
            IUserDAL dalmysql = container.ReSolve<IUserDAL>("mysql");
            IUserDAL dal = container.ReSolve<IUserDAL>();
            IUserBLL bll2 = container.ReSolve<IUserBLL>();

            ILcukContainer childcontainer1 = container.CreateChildContainer();
            IUserBLL childduserbll1  = childcontainer1.ReSolve<IUserBLL>();


            ILcukContainer childcontainer2 = container.CreateChildContainer();
            IUserBLL childduserbll2 = childcontainer2.ReSolve<IUserBLL>();

            Console.WriteLine(Object.ReferenceEquals(childduserbll1, childduserbll2));*/
            #endregion
            
            LuckContainer container = new LuckContainer();
            container.Register<IUserService, UserService>();
            //CustomAOPTest.show();
             IUserService userService1 = container.ReSolve<IUserService>();
            userService1.show1();
            userService1 = (IUserService) userService1.CreateProxtAOP(typeof(IUserService));
            userService1.show1();
        }

    }
}
