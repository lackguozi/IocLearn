using LuckFramework.CustomAop;
using System;
using System.Collections.Generic;
using System.Text;

namespace IocLearn.Interface
{
    public interface IUserService
    {
        void GetUser();
        
        void show();
        [LogBrfore]
        [LogAfter]
        void show1();
    }
}
