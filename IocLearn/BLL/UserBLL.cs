using IocLearn.Interface;

using LuckFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IocLearn
{
    public class UserBLL : IUserBLL
    {
        private IUserDAL _userdal;
        private IUserService _userservice;
        
        [LuckPropertyInjection]
        public IUserDAL PropUserdal { get; set; }
        [LuckConstructor]
        public UserBLL([LuckShortName("mysql")]IUserDAL userdal,IUserService userservice)
        {
            Console.WriteLine("构造函数构造");
            _userdal = userdal;
            _userservice = userservice;
        }

        public UserBLL(IUserDAL userdal)
        {
            _userdal = userdal;
        }

        public void GetUser()
        {
            Console.WriteLine("iuserbll getuser调用");
            _userdal.GetUser();
            _userservice.GetUser();
        }
    }
}
