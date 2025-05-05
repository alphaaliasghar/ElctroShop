using ElectonShop.Domain.Interfaces;
using ElectonShop.Domain.Models.User;
using ElectonShop.Domain.ViewModels;
using ElectonShop.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ElectonContext _context;

        public UserRepository(ElectonContext context)
        {
            _context = context;
        }

        public void DeleteUser(User user)
        {
            user.IsDelete = true;
            UpdateUser(user);
        }

        public void DeleteUser(int userId)
        {
            var user = GetUserbyId(userId);
            DeleteUser(user);

        }

        public bool ExistEmail(string email)
        {
           return  _context.Users
                .Any(u => u.Email == email);
        }

        public User GetUserForLogin(string emailOrusername, string password)
        {
            return _context.Users
                .SingleOrDefault(u => (u.Email == emailOrusername ||
                u.UserName == emailOrusername) && u.Password == password);
        }

        public bool ExistUsername(string username)
        {
            return _context.Users.Any(u => u.UserName == username);
        }

        public List<User> GetAllUser()
        {
            return _context.Users.ToList();
        }

        public User GetUserbyId(int id)
        {
            return _context.Users
                .FirstOrDefault(u => u.Id == id);
        }

        public void InsertUser(User user)
        {
            _context.Users.Add(user);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }

        public List<UserViewModel> GetAll(int take, int skip)
        {
           return _context.Users.Select(u=>new UserViewModel()
           {
               AvatarName = u.AvatarName,
               CreateDate = u.CreateDate,
               Email = u.Email,
               IsAdmin = u.IsAdmin,
               IsDelete = u.IsDelete,
               ModifiDate = u.ModifiDate,
               UserName=u.UserName,
               Id=u.Id,
           }).Skip(skip).Take(take).ToList();
        }

        public int CountPage()
        {
           return _context.Users.Count();
        }

        public bool EmailDuplicated(string email, int userId)
        {
            return _context.Users.
                 Any(u => u.Email == email && u.Id != userId);
        }

        public bool UserNameDuplicated(string username, int userId)
        {
            return _context.Users.Any(u => u.UserName == username
            && u.Id != userId);
        }
    }
}
