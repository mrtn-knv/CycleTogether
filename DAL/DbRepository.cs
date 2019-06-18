using Microsoft.EntityFrameworkCore;
using DAL.Contracts;
using System;
using System.Collections.Generic;
using DAL.Data;
using AutoMapper;
using DAL.Models;
using System.Linq;

namespace DAL
{
    public class DbRepository<TEntity> : IRepository<TEntity>, IDisposable  where TEntity : EntityBase
    {
        private readonly CycleTogetherDbContext _context;
        private DbSet<TEntity> dbSet;

        public DbRepository(CycleTogetherDbContext context) 
        {
            _context = context;
            dbSet = _context.Set<TEntity>();
        }

        public TEntity Create(TEntity entry)
        {
            _context.Add(entry);
            _context.SaveChanges();
            return entry;
        }

        public void Delete(Guid id)
        {
            var current = _context.Find<TEntity>(id);
            _context.Remove(current);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Edit(TEntity entry)
        {
            _context.Update(entry);
            _context.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet;
        }

        public TEntity GetById(Guid id)
        {
           return dbSet.FirstOrDefault(x => x.Id == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
