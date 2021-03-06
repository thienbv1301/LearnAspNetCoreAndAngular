﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Web.Repository.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> FindByIdAsync(object id);
        void Add(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
    }
}
