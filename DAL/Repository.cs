using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private static List<T> context;


        public Repository(List<T> _context)
        {
            context = _context;
           
        }

        public T Create(T entry)
        {
            context.Add(entry);
            return entry;
        }

        public void Delete(T entry)
        {
            context.Remove(entry);
        }

        public T Edit(T entry)
        {
            T updated = entry;
            return updated;
        }
    }
}
