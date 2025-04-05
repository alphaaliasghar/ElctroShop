using ElctroShop.Domain.Interfaces;
using ElctroShop.Domain.Models.User;
using ElctroShop.Domain.ViewModels;
using ElctroShop.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ElctroShopContext _context;

        public UserRepository(ElctroShopContext context)
        {
            _context = context;
        }

        public void DeleteUser(int userId)
        {
            var user = GetByUserId(userId);
            DeleteUser(user);
        }

        public void DeleteUser(User user)
        {
            user.IsDelete = true;
            UpdateUser(user);
        }

        public bool EmailDuplicated(string email, int userId)
        {
            return _context.Users.Any(u => u.Email == email && u.Id != userId);
        }

        public bool EmailExist(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public List<UserViewModel> GetAll(int take, int skip)
        {
           return _context.Users
                .Select(u=>new UserViewModel()
                {
                    Id=u.Id,
                    AvatarName=u.AvatarName,
                    CreateDate=u.CreateDate,
                    Email=u.Email,
                    IsAdmin=u.IsAdmin,
                    IsDelete=u.IsDelete,
                    ModifiDate=u.ModifiDate,
                    UserName=u.UserName,
                }).Skip(skip).Take(take).ToList();
        }

        public User GetByUserId(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.Id == userId);
        }

        public User GetUserForLogin(string usernameOremail, string password)
        {
            return _context.Users
                   .SingleOrDefault(u => (u.Email == usernameOremail || u.UserName == usernameOremail)
                   && u.Password == password);
        }

        public void InsertUser(User user)
        {
            _context.Users.Add(user);
        }

        public int PageCount()
        {
            return _context.Users.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }

        public bool UserNameDuplicated(string username, int userId)
        {
            return _context.Users.Any(u => u.UserName == username &&
            u.Id != userId);
        }

        public bool UserNameExist(string username)
        {
            return _context.Users.Any(u => u.UserName == username);
        }
    }
}
