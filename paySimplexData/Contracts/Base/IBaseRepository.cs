using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace paySimplexData.Contracts.Base
{
    public interface IBaseRepository<T> where T : class
    {
        void Create(T entity, long userId);
        void Update(T entity, long userId);
        void Delete(T entity);
        T Get(Expression<Func<T, bool>> where, List<string> includes = null);
        List<T> GetAll(List<string> includes = null);
        List<T> GetMany(Expression<Func<T, bool>> where, List<string> includes = null);
        void SaveChanges();
    }
}
