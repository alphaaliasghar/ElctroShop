using ElectonShop.Application.Services.Interface;
using ElectonShop.Domain.Enums;
using ElectonShop.Domain.Interfaces;
using ElectonShop.Domain.Models.User;
using ElectonShop.Domain.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TopLearn.Core.Security;

namespace ElectonShop.Application.Services.Implemation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ResultChangePassword ChangePassword(int userId, ChangePaswordViewModel model)
        {
            var user = _userRepository.GetUserbyId(userId);

            string oldhshpasword = PasswordHelper.EncodePasswordMd5(model.OldPasword);
            if (user.Password != oldhshpasword)
            {
                return ResultChangePassword.OldPasswordNotValid;
            }
            user.Password = PasswordHelper.EncodePasswordMd5(model.Password);
            _userRepository.UpdateUser(user);
            _userRepository.Save();
            return ResultChangePassword.Success;
        }

        public int CountPage()
        {
            return _userRepository.CountPage();
        }

        public ResultCreateUser CreateUser(CreateUserViewModel model)
        {
            model.UserName = model.UserName.TrimEnd();
            model.Email = model.Email.ToLower().Trim();
            if (_userRepository.ExistUsername(model.UserName))
                return ResultCreateUser.UserNameNotValid;
            if (_userRepository.ExistEmail(model.Email))
                return ResultCreateUser.EmailNotValid;

            User user = new User()
            {
                AvatarName = model.UserName,
                CreateDate = DateTime.Now,
                Email = model.Email,
                Password = PasswordHelper.EncodePasswordMd5(model.Password),
                IsAdmin = model.IsAdmin,
                IsDelete = model.IsDelete,
                UserName = model.UserName,
            };
            #region Manage Image
            if (model.Avatar != null)
            {
                user.AvatarName = Guid.NewGuid().ToString()
                    + Path.GetExtension(model.Avatar.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/Avatar", user.AvatarName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.Avatar.CopyTo(stream);
                }
            }
            #endregion

            _userRepository.InsertUser(user);
            _userRepository.Save();
            return ResultCreateUser.Success;
        }

        public bool Delete(int id)
        {
            var user = _userRepository.GetUserbyId(id);
            if (user == null)
                return false;
            user.IsDelete = true;
            _userRepository.UpdateUser(user);
            _userRepository.Save();
            return true;

        }

        public ResultEditInformation EditInformation(EditInformationViewModel model)
        {
            var user = _userRepository.GetUserbyId(model.Id);
            if (user == null)
            {
                return ResultEditInformation.UserInformationNotFound;
            }

            user.UserName = model.UserName;
            user.Email = model.Email;
            user.ModifiDate = model.Modifidate;

            #region Manage Image

            if (model.Avatar != null)
            {
                if (user.AvatarName != "1.jpg")
                {
                    string deletepath = Guid.NewGuid().ToString()
                        + Path.GetExtension(model.AvatarName);
                    if (System.IO.File.Exists(deletepath))
                    {
                        System.IO.File.Delete(deletepath);
                    }
                }
                user.AvatarName = Guid.NewGuid().ToString()
                    + Path.GetExtension(model.Avatar.FileName);

                string path = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/Avatar", user.AvatarName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.Avatar.CopyTo(stream);
                }
            }
            #endregion

            _userRepository.UpdateUser(user);
            _userRepository.Save();
            return ResultEditInformation.Success;
        }

        public ResultEditUser EditUser(EditUserViewModel model)
        {
            var user = _userRepository.GetUserbyId(model.Id);
            if (user == null)
            {
                return ResultEditUser.UserNotFound;
            }
            model.UserName = model.UserName.Trim();
            model.Email = model.Email.ToLower().Trim();

            if (_userRepository.EmailDuplicated(model.Email, user.Id))
            {
                return ResultEditUser.EmailDuplicated;
            }
            if (_userRepository.UserNameDuplicated(model.UserName, user.Id))
            {
                return ResultEditUser.UserNameDuplicated;
            }
            user.Email = model.Email;
            user.UserName = model.UserName;
            user.AvatarName = model.AvatarName;
            user.ModifiDate = DateTime.Now;
            user.IsAdmin=model.IsAdmin;
            user.IsDelete=model.IsDelete;

            #region Manage Image
            if (model.Avatar != null)
            {
                if (user.AvatarName != "1.jpg")
                {
                    string deletepath = Guid.NewGuid().ToString()
                          + Path.GetExtension(model.Avatar.FileName);
                    if (System.IO.File.Exists(deletepath))
                    {
                        System.IO.File.Delete(deletepath);
                    }
                }
                user.AvatarName = Guid.NewGuid().ToString()
                    + Path.GetExtension(model.Avatar.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/Avatar", user.AvatarName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.Avatar.CopyTo(stream);
                }
            }
            #endregion 

            if (!string.IsNullOrEmpty(model.NewPassword))
            {
                user.Password = PasswordHelper.EncodePasswordMd5(model.NewPassword);
            }
            _userRepository.UpdateUser(user);
            _userRepository.Save();
            return ResultEditUser.Success;
        }

        public List<UserViewModel> GetAll(int take, int skip)
        {
            return _userRepository.GetAll(take, skip);

        }

        public UserViewModel GetForDetail(int id)
        {
            var user = _userRepository.GetUserbyId(id);
            if (user == null)
            {
                return null;
            }
            return new UserViewModel()
            {
                AvatarName = user.AvatarName,
                CreateDate = DateTime.Now,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                Id = id,
                IsDelete = user.IsDelete,
                ModifiDate = DateTime.Now,
                UserName = user.UserName,
            };
        }

        public EditInformationViewModel GetForEdit(int id)
        {
            var user = _userRepository.GetUserbyId(id);
            if (user == null)
            {
                return null;
            }
            return new EditInformationViewModel()
            {
                AvatarName = user.AvatarName,
                Email = user.Email,
                Id = id,
                UserName = user.UserName,
            };
        }

        public EditUserViewModel GetForEditUser(int id)
        {
            var user = _userRepository.GetUserbyId(id);

            if (user == null)

                return null;

            return new EditUserViewModel()
            {
                AvatarName = user.AvatarName,
                Email = user.Email,
                Id = id,
                IsAdmin = user.IsAdmin,
                IsDelete = user.IsDelete,
                UserName = user.UserName,
                


            };

        }

        public InformationViewModel GetInformationForShow(int id)
        {
            var information = _userRepository.GetUserbyId(id);
            if (information == null)
                return null;
            return new InformationViewModel()
            {
                AvatarName = information.AvatarName,
                CreateDate = information.CreateDate,
                Email = information.Email,
                ModifiDate = information.ModifiDate,
                UserName = information.UserName,
                Id = information.Id,
            };
        }

        public User LoginUser(LoginViewModel model)
        {
            string hashpassword = PasswordHelper.EncodePasswordMd5(model.Password);

            var user = _userRepository.GetUserForLogin(model.EmailOrUserName,
                hashpassword);
            return user;
        }

        public ResultRegisterUser RegisterUser(RegisterViewModel model)
        {
            model.UserName = model.UserName.Trim();
            model.Email = model.Email.ToLower().Trim();
            if (_userRepository.ExistEmail(model.Email))
            {
                return ResultRegisterUser.EmailNotValid;
            }
            if (_userRepository.ExistUsername(model.UserName))
            {
                return ResultRegisterUser.UserNameNotValid;
            }
            User users = new User()
            {
                CreateDate = DateTime.Now,
                AvatarName = "1.jpg",
                IsAdmin = false,
                IsDelete = false,
                Email = model.Email,
                Password = PasswordHelper.EncodePasswordMd5(model.Password),
                UserName = model.UserName,

            };
            _userRepository.InsertUser(users);
            _userRepository.Save();
            return ResultRegisterUser.Success;
        }
    }
}
