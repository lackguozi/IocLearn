using System;
using System.Collections.Generic;
using System.Text;

namespace LuckFramework
{
    public interface ILcukContainer
    {
        void Register<TFrom, TTo>(string shortname=null, LifetimeType lifetimeType=LifetimeType.Transient)  where TTo : TFrom;
        TFrom ReSolve<TFrom>(string shortname = null);                                         

    }
}
