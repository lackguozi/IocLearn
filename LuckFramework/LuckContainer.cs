using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;


namespace LuckFramework
{
    public class LuckContainer : ILcukContainer
    {
        private Dictionary<string, LuckContainerModel> lcukdic = new Dictionary<string, LuckContainerModel>();
        private Dictionary<string,Object> luckScopeContainer = new Dictionary<string, Object>();
        public LuckContainer()
        {
        }
        private LuckContainer(Dictionary<string, LuckContainerModel> lcukdic, Dictionary<string, Object> luckScopeContainer)
        {
            this.lcukdic = lcukdic;
            this.luckScopeContainer = luckScopeContainer;
        }
        public ILcukContainer CreateChildContainer()
        {
            return new LuckContainer(this.lcukdic, new Dictionary<string, object>());
            
        }
        public void Register<TFrom,TTo>(string shortname=null,LifetimeType lifetimeType= LifetimeType.Transient)where TTo:TFrom
        {
            this.lcukdic.Add(this.GetKey(typeof(TFrom).FullName,shortname),new LuckContainerModel {
            TargetType=typeof(TTo),
            Lifetime=lifetimeType,
            });
        }

        public TFrom ReSolve<TFrom>(string shortname=null)
        {
            return (TFrom)ReSolveObject(typeof(TFrom),shortname);
        }
        private object ReSolveObject(Type abstractType,string shortname=null)
        {
            string key =this.GetKey(abstractType.FullName,shortname);
            var type = lcukdic[key];
            switch (type.Lifetime)
            {
                case LifetimeType.Transient:
                    break;
                case LifetimeType.Singleton:
                    if (type.SingleInstance != null)
                    {
                        return type.SingleInstance;
                    }
                    break;
                case LifetimeType.Scope:
                    if(luckScopeContainer[key] != null)
                    {
                        return luckScopeContainer[key];
                    }
                    break;
                default:
                    break;
            }
            #region 选择合适的构造函数 选择参数最多的那个 特性标记的
            ConstructorInfo con = null;
            var cons = type.TargetType.GetConstructors();
            con = cons.FirstOrDefault(a => a.IsDefined(typeof(LuckConstructorAttribute), true));
            if(con == null)
            {
                con = type.TargetType.GetConstructors().OrderByDescending(a => a.GetParameters().Length).FirstOrDefault();
            }
             
            #endregion
            List<object> ls = new List<object>();
            #region 开始构造函数
            foreach(var para in con.GetParameters())//获取参数的构造参数 
            {
                Type  paratype = para.ParameterType;
                string parashortname = this.GetShortName(para);
                object paraInstance = ReSolveObject(paratype, parashortname);//依次构造参数
                ls.Add(paraInstance);
            }
            #endregion
            object res =  Activator.CreateInstance(type.TargetType, ls.ToArray());
            foreach (var prop in type.TargetType.GetProperties().Where(a => a.IsDefined(typeof(LuckPropertyInjectionAttribute), true)))
            {
                
                Type proptype = prop.PropertyType;
                string proptypshortename = this.GetShortName2(prop);
                object propInstance = ReSolveObject(proptype);
                prop.SetValue(res, propInstance);
                
                
            }
            switch (type.Lifetime)
            {
                case LifetimeType.Transient:
                    break;
                case LifetimeType.Singleton:
                    if(type.SingleInstance == null)
                    {
                        type.SingleInstance = res;
                    }
                    break;
                case LifetimeType.Scope:
                    luckScopeContainer[key] = res;
                  
                    break;
                default:
                    break;
            }
            return res;
            
        }
        private string GetKey(string fullname,string shortname)
        {
            return fullname + "__" + shortname;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string GetShortName(ParameterInfo info)
        {
            if (info.IsDefined(typeof(LuckShortNameAttribute), true))
            {
                return info.GetCustomAttribute<LuckShortNameAttribute>().shortname;
            }
            return null;
        }
        private string GetShortName2(PropertyInfo info)
        {
            if (info.IsDefined(typeof(LuckShortNameAttribute), true))
            {
                return info.GetCustomAttribute<LuckShortNameAttribute>().shortname;
            }
            return null;
        }
        private string getshortnameall(ICustomAttributeProvider iprovider)
        {
            if (iprovider.IsDefined(typeof(LuckShortNameAttribute), true))
            {
                var attribute = (LuckShortNameAttribute)iprovider.GetCustomAttributes(typeof(LuckShortNameAttribute), true)[0];
                return attribute.shortname;
            }
            return null;
               
        }
    }
}
