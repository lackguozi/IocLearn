using System;
using System.Collections.Generic;
using System.Text;

namespace LuckFramework
{
    public class LuckContainerModel
    {
        public Type TargetType { get; set; }
        public LifetimeType Lifetime { get; set; }
        public object SingleInstance { get; set; }
    }
    public enum LifetimeType
    { 
        Transient,
        Singleton,
        Scope
    }

}
