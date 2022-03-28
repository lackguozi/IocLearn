using IocLearn.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace IocLearn.DAL
{
    public class UserDALMysql : IUserDAL
    {
        private IUserService userservice;
        public UserDALMysql(IUserService userservice)
        {
            this.userservice = userservice;
        }

        public void GetUser()
        {
            Console.WriteLine("mysql getuser调用");
        }
    }
}
