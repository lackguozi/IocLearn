using System;
using System.Collections.Generic;
using System.Text;

namespace LuckFramework
{
    [AttributeUsage(AttributeTargets.Parameter| AttributeTargets.Property)]
    public class LuckShortNameAttribute : Attribute
    {
        public string shortname { get; set; }
        public LuckShortNameAttribute(string name)
        {
            this.shortname = name;
        }
        
    }
}
