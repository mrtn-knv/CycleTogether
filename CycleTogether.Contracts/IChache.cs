using System;
using System.Collections.Generic;

namespace CycleTogether.Contracts
{
    public interface IChache<T> where T : class
    {
        void AddItem(T item);
        T GetItem(string id);
        void RemoveItem(T item);
        IEnumerable<T> All();
        IEnumerable<T> AllBy(string Id);
        void AddAll(List<T> items);


    }
}
