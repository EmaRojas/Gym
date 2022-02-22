using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Generals
{
    public interface IGeneralService<T>
    {
        List<T> GetByDni(Expression<Func<T, bool>> expression);
        List<T> GetByToken(Expression<Func<T, bool>> expression);
        List<T> GetByCriteria(Expression<Func<T, bool>> expression);
        List<T> GetUser(Expression<Func<T, bool>> expression);
        List<T> GetByEmail(Expression<Func<T, bool>> expression);
        List<T> GetByCriteriaByUser(Expression<Func<T, bool>> expression);
        List<T> GetAllByUser();
        T GetByIdByUser(int id);
        T GetById(int id);
        List<T> GetAll();

        void Delete(T entity);
        void Update(T entity);
        void Create(T entity);
    }
}
