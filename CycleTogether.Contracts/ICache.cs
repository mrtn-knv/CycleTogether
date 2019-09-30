using System;
using System.Collections.Generic;
using System.Text;
using WebModels;

namespace CycleTogether.Contracts
{
    public interface ICache<T> where T : class
    {
        void Add(T route);
        T Update(T route);
        void Remove(string routeId);
        T Get(string routeId);
    }
}
