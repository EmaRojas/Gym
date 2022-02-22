using App.Services.Contract.Models;
using System.Collections.Generic;

namespace App.Services.Contract
{
    public interface IUserService
    {
        IEnumerable<UserViewModel> GetAll();

        IEnumerable<UserViewModel> GetById();

        IEnumerable<UserViewModel> GetByUsername(string username);

        void Insert(UserViewModel model);

        void Update(UserViewModel model);

        void Delete(UserViewModel model);
    }
}
