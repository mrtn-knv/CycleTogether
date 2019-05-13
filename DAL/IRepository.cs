using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IRepository<T> where T : class
    {
        T Create(T context);
        T Edit(T context);
        void Delete(T context);
       
    }
}
