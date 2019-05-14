using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IRepository<T> where T : EntityBase
    {
        //methods as Tasks to all repos and services
        T GetById(Guid id);
        T Create(T entry);
        void Edit(T entry);
        void Delete(Guid id);
       
    }
}
