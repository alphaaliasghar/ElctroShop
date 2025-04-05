using ElctroShop.Domain.Models.User;
using ElctroShop.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Domain.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAll();

        User GetByUserId(int userId);

        void InsertUser(User user);

        void Save();

        void UpdateUser(User user);

        void DeleteUser(int userId);

        void DeleteUser(User user);

        bool EmailExist(string email);

        bool UserNameExist(string username);

        User GetUserForLogin(string usernameOremail, string password);

        int PageCount();

        List<UserViewModel> GetAll(int take, int skip);

        bool EmailDuplicated(string email, int userId);
        bool UserNameDuplicated(string username, int userId);
    
    }
}
