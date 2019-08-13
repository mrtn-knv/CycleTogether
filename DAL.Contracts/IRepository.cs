using DAL.Models;
using System;
using System.Collections.Generic;

namespace DAL.Contracts
{
    public interface IRepository<T> where T : EntityBase
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        T Create(T entry);
        void Edit(T entry);
        void Delete(Guid id);
        void SaveChanges();

    }
}
