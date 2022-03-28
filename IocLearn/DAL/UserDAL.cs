using IocLearn.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace IocLearn.BLL
{
    internal class UserDAL : IUserDAL
    {
        private IUserService _userservice;
        public UserDAL(IUserService userservice)
        {
            this._userservice = userservice;
        }

        public void GetUser()
        {
            Console.WriteLine("userdal getuser调用");
        }
    }
}
