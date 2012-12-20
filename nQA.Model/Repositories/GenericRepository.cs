using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using nQA.Model.Interfaces;

namespace nQA.Model.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class, new()
    {
        public IEnumerable<T> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public T FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(T newEntity)
        {
            throw new NotImplementedException();
        }

        public void Remove(T entity)
        {
            throw new NotImplementedException();
        }
    }
}