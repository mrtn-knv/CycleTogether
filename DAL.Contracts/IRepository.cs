using DAL.Models;
using System;


namespace DAL.Contracts
{
    public interface IRepository<T> where T : EntityBase
    {
        T GetById(Guid id);
        T Create(T entry);
        void Edit(T entry);
        void Delete(Guid id);

    }
}
