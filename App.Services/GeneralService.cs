using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using App.Core;
using App.Core.Common;
using App.Core.Infrastructure;
using App.Services;

namespace App.Services.Generals
{
    public class GeneralService<T> : IGeneralService<T> where T : BaseEntity
    {
        #region Fields

        private readonly IRepository<T> _repository;

        #endregion

        #region Ctor

        public GeneralService(IRepository<T> repository)
        {
            this._repository = repository;
        }

        #endregion

        public List<T> GetByCriteria(Expression<Func<T, bool>> expression)
        {
            return _repository.Table().Where(expression).ToList();
        }

        public List<T> GetByCriteriaByUser(Expression<Func<T, bool>> expression)
        {
            if (UserData.UserId == null)
            {
                var list = new List<T>();
                return list;
            }
            return _repository.Table().Where(x => x.UserId == UserData.UserId && x.DeletedDate == null).Where(expression).ToList();
        }

        public List<T> GetUser(Expression<Func<T, bool>> expression)
        {
            return _repository.Table().Where(expression).ToList();
        }

        public List<T> GetByEmail(Expression<Func<T, bool>> expression)
        {
            return _repository.Table().Where(expression).ToList();
        }

        public List<T> GetByDni(Expression<Func<T, bool>> expression)
        {
            return _repository.Table().Where(expression).ToList();
        }

        public List<T> GetByToken(Expression<Func<T, bool>> expression)
        {
            return _repository.Table().Where(expression).ToList();
        }

        public List<T> GetAll()
        {
            return _repository.Table().ToList();
        }

        public List<T> GetAllByUser()
        {
            return _repository.Table().Where(x => x.UserId == UserData.UserId && x.DeletedDate == null).ToList();
        }

        public T GetById(int id)
        {
            return _repository.Table().FirstOrDefault(x => x.Id == id && x.DeletedDate == null);
        }

        public T GetByIdByUser(int id)
        {
            return _repository.Table().FirstOrDefault(x => x.UserId == UserData.UserId && x.Id == id && x.DeletedDate == null);
        }

        #region Create/Update/Delete

        public void Create(T Obj)
        {
            if (Obj != null)
            {
                Obj.UserId = UserData.UserId;
                _repository.Insert(Obj);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public void Update(T Obj)
        {
            if (Obj != null)
            {
                try
                {
                    _repository.Update(Obj);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public void Delete(T Obj)
        {
            if (Obj != null)
            {
                _repository.Delete(Obj);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
