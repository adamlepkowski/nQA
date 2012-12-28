using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace nQA.Model.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> FindAll();
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        T FindById(int id);
        void Add(T newEntity);
        void Remove(T entity);
        void SaveChanges();
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
    }
}