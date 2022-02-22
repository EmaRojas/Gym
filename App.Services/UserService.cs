using App.Core.Common;
using App.Core.Domain;
using App.Core.Infrastructure;
using App.Services.Contract;
using App.Services.Contract.Models;
using System.Collections.Generic;
using System.Linq;

namespace App.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;

        public UserService(
            IUnitOfWork unitOfWork,
            IRepository<User> userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public IEnumerable<UserViewModel> GetAll()
        {
            return _userRepository
                .GetAll()
                .Select(Map);
        }

        public IEnumerable<UserViewModel> GetById()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<UserViewModel> GetByUsername(string username)
        {
            var predicate = PredicateBuilder.True<User>();
            predicate = predicate.And(u => u.UserName.Contains(username));

            return _userRepository
                .GetByCriteria(predicate)
                .Select(Map);
        }

        public void Insert(UserViewModel model)
        {
            var entity = Map(model);

            _userRepository.Insert(entity);
            _unitOfWork.Commit();
        }

        public void Update(UserViewModel model)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(UserViewModel model)
        {
            throw new System.NotImplementedException();
        }

        private UserViewModel Map(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                Username = user.UserName
            };
        }

        private User Map(UserViewModel model)
        {
            return new User
            {
                UserName = model.Username,
                Password = model.Password
            };
        }
    }
}
