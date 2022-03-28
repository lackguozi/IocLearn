using System;
using System.Collections.Generic;
using System.Text;

namespace LuckFramework
{
    internal interface ILcukContainer
    {
        void Register<TFrom, TTo>(string shortname=null)  where TTo : TFrom;
        TFrom ReSolve<TFrom>(string shortname = null);                                         

    }
}
