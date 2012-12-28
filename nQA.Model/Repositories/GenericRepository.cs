using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

using nQA.Model.Interfaces;

namespace nQA.Model.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class, new()
    {
        private readonly DatabaseContext _databaseContext;

        public GenericRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IEnumerable<T> FindAll()
        {
            return _databaseContext.Set<T>().ToList();
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _databaseContext.Set<T>().Where(predicate).ToList();
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _databaseContext.Set<T>().FirstOrDefault(predicate);
        }

        public T FindById(int id)
        {
            return _databaseContext.Set<T>().Find(id);
        }

        public void Add(T newEntity)
        {
            _databaseContext.Set<T>().Add(newEntity);
        }

        public void Update(T entity)
        {
            _databaseContext.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            _databaseContext.Set<T>().Remove(entity);
        }

        public void SaveChanges()
        {
            _databaseContext.SaveChanges();
        }
    }
}