using Microsoft.EntityFrameworkCore;
using paySimplexData.Context;
using paySimplexData.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace paySimplexData.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entities.Base
    {
        private readonly paySimplexContext dataContext;
        private readonly DbSet<T> dbSet;

        public BaseRepository(paySimplexContext dbContext)
        {
            dataContext = dbContext;
            dbSet = dataContext.Set<T>();
        }

        public void Create(T entity, long userId)
        {
            dbSet.Add(entity);
            DoBeforeSaving(userId);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
            dataContext.Entry(entity).State = EntityState.Deleted;
            SaveChanges();
        }

        public T Get(Expression<Func<T, bool>> where, List<string> includes = null)
        {
            var query = dbSet.Select(x => x).Where(where);
            if (includes != null)
                includes.ForEach(x => query = query.Include(x));

            return query.FirstOrDefault();
        }

        public List<T> GetAll(List<string> includes = null)
        {
            var query = dbSet.Select(x => x);
            if (includes != null)
                includes.ForEach(x => query = query.Include(x));

            return query.ToList();
        }

        public T GetById(long id)
        {
            return dbSet.Find(id);
        }

        public List<T> GetMany(Expression<Func<T, bool>> where, List<string> includes = null)
        {
            var query = dbSet.Select(x => x).Where(where);
            if (includes != null)
                includes.ForEach(x => query = query.Include(x));

            return query.ToList();
        }

        public void SaveChanges()
        {
            dataContext.SaveChanges();
        }

        public void Update(T entity, long userId)
        {
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
            DoBeforeSaving(userId);
        }

        private void DoBeforeSaving(long userId)
        {
            var entries = dataContext.ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is Entities.Base trackable)
                {
                    var dateNow = DateTime.Now;
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.UpdateDate = dateNow;
                            trackable.UpdatedBy = userId;
                            break;
                        case EntityState.Added:
                            trackable.CreateDate = dateNow;
                            trackable.CreatedBy = userId;
                            break;
                        default:
                            break;
                    }
                }
            }

            SaveChanges();
        }
    }
}
