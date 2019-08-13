using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Contracts;
using DAL.Models;

namespace DAL
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {

        protected static List<T> context = new List<T>();

        public Repository()
        {

        }

        public T Create(T entry)
        {
            entry.Id = Guid.NewGuid();
            context.Add(entry);
            return entry;
        }

        public void Delete(Guid id)
        {
            var current = context.FirstOrDefault(e => e.Id == id);
            if (current != null)
            {
                context.Remove(current);
            }
            
        }

        public void Edit(T entry)
        {
            var toEdit = context.FirstOrDefault(e => e.Id == entry.Id);
            if (toEdit != null)
            {
                toEdit = entry;
            }
            
        }

        public IEnumerable<T> GetAll()
        {
           return context.ToList();
        }

        public T GetById(Guid id)
        {
          return context.FirstOrDefault(t => t.Id == id);
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
