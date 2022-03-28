using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;


namespace LuckFramework
{
    public class LuckContainer : ILcukContainer
    {
        private Dictionary<string,Type> lcukdic = new Dictionary<string,Type>();
        public void Register<TFrom,TTo>(string shortname=null)where TTo:TFrom
        {
            this.lcukdic.Add(this.GetKey(typeof(TFrom).FullName,shortname),typeof(TTo));
        }

        public TFrom ReSolve<TFrom>(string shortname=null)
        {
            return (TFrom)ReSolveObject(typeof(TFrom),shortname);
        }
        private object ReSolveObject(Type abstractType,string shortname=null)
        {
            string key =this.GetKey(abstractType.FullName,shortname);
            Type type = lcukdic[key];
            #region 选择合适的构造函数 选择参数最多的那个 特性标记的
            ConstructorInfo con = null;
            var cons = type.GetConstructors();
            con = cons.FirstOrDefault(a => a.IsDefined(typeof(LuckConstructorAttribute), true));
            if(con == null)
            {
                con = type.GetConstructors().OrderByDescending(a => a.GetParameters().Length).FirstOrDefault();
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
            object res =  Activator.CreateInstance(type, ls.ToArray());
            foreach (var prop in type.GetProperties().Where(a => a.IsDefined(typeof(LuckPropertyInjectionAttribute), true)))
            {
                //Console.WriteLine(prop.Name);
                Type proptype = prop.PropertyType;
                string proptypshortename = this.GetShortName2(prop);
                object propInstance = ReSolveObject(proptype);
                prop.SetValue(res, propInstance);
                
                //Console.WriteLine(prop.Name);
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
