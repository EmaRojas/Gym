using App.Core.Common;
using App.Core.Infrastructure;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace App.Data.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly UnitOfWork _unitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
        }

        protected ISession Session => _unitOfWork.Session;

        public IEnumerable<T> GetAll()
        {
            return Session.Query<T>();
        }

        public IQueryable<T> Table()
        {
            return Session.Query<T>();
        }

        public IEnumerable<T> GetByCriteria(Expression<Func<T, bool>> predicate)
        {
            return Session.Query<T>().Where(predicate);
        }

        public T GetById(int id)
        {
            return Session.Query<T>().Where(x => x.Id == id).FirstOrDefault();
        }

        public void Insert(T entity)
        {
            Session.Save(entity);
        }

        public void Update(T entity)
        {
            Session.Update(entity);
        }

        public void Delete(T entity)
        {
            Session.Delete(entity);
        }
    }
}
