using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace paySimplexBusiness.Contracts
{
    public interface IBaseContract<T> where T : class
    {
        object Create(T obj, long userId);
        object Update(T obj, long userId);
        object Delete(long id);
        object Get();
        object GetById(long id);
        object GetByName(string name);
    }
}
