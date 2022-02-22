using App.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace App.Core.Infrastructure
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Table();
        IEnumerable<T> GetAll();
        
        IEnumerable<T> GetByCriteria(Expression<Func<T, bool>> predicate);

        T GetById(int id);

        void Insert(T entity);

        void Update(T entity);
        
        void Delete(T entity);
    }
}
