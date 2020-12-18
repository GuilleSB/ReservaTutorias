using Backend.DAL.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Backend.DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly BackendDbContext _dbContext;
        public Repository(BackendDbContext context)
        {
            _dbContext = context;
        }

        public IQueryable<T> AsQueryble()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            try
            {
                _dbContext.Entry<T>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            }
            catch (Exception ee)
            {
            }
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>();
        }

        public T GetOne(Expression<Func<T, bool>> predicado)
        {
            return _dbContext.Set<T>().Where(predicado).FirstOrDefault();
        }

        public T GetOneById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public void Insert(T entity)
        {
            if (_dbContext.Entry<T>(entity).State == EntityState.Detached)
            {
                _dbContext.Entry<T>(entity).State = EntityState.Added;
            }
            else
            {
                _dbContext.Set<T>().Add(entity);
            }
        }

        public IEnumerable<T> Search(Expression<Func<T, bool>> predicado)
        {
            return _dbContext.Set<T>().Where(predicado);
        }

        public void Update(T entity)
        {
            if (_dbContext.Entry<T>(entity).State == EntityState.Detached)
            {
                _dbContext.Set<T>().Attach(entity);
            }
            _dbContext.Entry<T>(entity).State = EntityState.Modified;
        }
    }
}
