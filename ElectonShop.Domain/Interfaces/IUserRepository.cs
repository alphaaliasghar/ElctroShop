using ElectonShop.Domain.Models.User;
using ElectonShop.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Domain.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAllUser();

        User GetUserbyId(int id);

        void InsertUser(User user);

        void UpdateUser(User user);

        void DeleteUser(User user);
        void DeleteUser(int userId);

        void Save();

        bool ExistEmail(string email);

        bool ExistUsername(string username);

        User GetUserForLogin(string emailOrusername, string password);


        int CountPage();
       List<UserViewModel> GetAll(int take,int skip);

        bool EmailDuplicated(string email, int userId);

        bool UserNameDuplicated(string username, int userId);
    }
}
