using IocLearn.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace IocLearn.Services
{
    public class UserService : IUserService
    {
        public UserService()
        {
            //Console.WriteLine("UserService 构造");
        }

        public void GetUser()
        {
            Console.WriteLine("UserService getuser调用");
        }
        public void show()
        {
            Console.WriteLine("你好我很想你");
        }
        public void show1()
        {
            Console.WriteLine("show  111121");
        }
    }
}
