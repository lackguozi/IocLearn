using IocLearn.BLL;
using IocLearn.DAL;
using IocLearn.Interface;
using IocLearn.Services;
using LuckFramework;
using System;

namespace IocLearn
{
    internal class Program
    {
        static void Main(string[] args)
        {
             LuckContainer container = new LuckContainer();
            container.Register<IUserBLL,UserBLL>();
            container.Register<IUserDAL, UserDAL>();
            container.Register<IUserService, UserService>();
            container.Register<IUserDAL, UserDALMysql>("mysql");
            IUserBLL bll = container.ReSolve<IUserBLL>();
            IUserDAL dal = container.ReSolve<IUserDAL>();
            
            dal.GetUser();
            Console.WriteLine("*****************");
            bll.GetUser();
            
        }
        
    }
}
