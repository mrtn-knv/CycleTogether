using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IRepository<T> where T : class
    {
        //methods as Tasks to all repos and services
        T Create(T entry);
        T Edit(T entry);
        void Delete(T entry);
       
    }
}
