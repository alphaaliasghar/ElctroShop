using ElctroShop.Application.Services.Interfaces;
using ElctroShop.Domain.Enums;
using ElctroShop.Domain.Interfaces;
using ElctroShop.Domain.Models.User;
using ElctroShop.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Security;

namespace ElctroShop.Application.Services.Implemation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ResultChanePassword ChanePassword(int userid, ChangePasswordViewModel model)
        {
            var user = _userRepository.GetByUserId(userid);

            string oldhshpasword = PasswordHelper.EncodePasswordMd5(model.OldPassword);
            if (user.Password != oldhshpasword)
            {
                return ResultChanePassword.OldPasswordNotValid;
            }
            user.Password = PasswordHelper.EncodePasswordMd5(model.Password);
            _userRepository.UpdateUser(user);
            _userRepository.Save();
            return ResultChanePassword.Success;

        }

        public ResultCreateUser CreateUser(CreateUserViewModel model)
        {
            model.UserName = model.UserName.Trim();
            model.Email = model.Email.ToLower().Trim();

            if (_userRepository.UserNameExist(model.UserName))
                return ResultCreateUser.UserNameNotValid;
            if (_userRepository.EmailExist(model.Email))
                return ResultCreateUser.EmailNotValid;

            User user = new User()
            {
                CreateDate = DateTime.Now,
                Email = model.Email,
                IsAdmin = model.IsAdmin,
                IsDelete = false,
                AvatarName = "1.jpg",
                Password = PasswordHelper.EncodePasswordMd5(model.Password),
                UserName = model.UserName,

            };
            #region Manage Image User

            if (model.Avatar != null)
            {

                user.AvatarName = Guid.NewGuid().ToString() +
                Path.GetExtension(model.Avatar.FileName);

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

        public bool DeleteUser(int id)
        {
            var user = _userRepository.GetByUserId(id);
            if (user == null)
                return false;


            user.IsDelete = true;
            _userRepository.UpdateUser(user);
            _userRepository.Save();

            return true;
        }

        public ResultEditAccount EditAccount(EditAccountViewModel model)
        {
            var edit = _userRepository.GetByUserId(model.Id);
            if (edit == null)
                return ResultEditAccount.UserAccountNotFound;


            edit.UserName = model.Username;
            edit.Email = model.Email;
            edit.ModifiDate=DateTime.Now;

            #region ManageImage

            if (model.Avatar != null)
            {
                if (edit.AvatarName != "1.jpg")
                {
                    string deletepath = Guid.NewGuid().ToString() +
                         Path.GetExtension(edit.AvatarName);
                    if (System.IO.File.Exists(deletepath))
                    {
                        System.IO.File.Delete(deletepath);
                    }
                }
                edit.AvatarName = Guid.NewGuid().ToString() +
                      Path.GetExtension(model.Avatar.FileName);

                string path = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/Avatar", edit.AvatarName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.Avatar.CopyTo(stream);
                }
                
            }
            else
            {
                edit.AvatarName = model.AvatarName;
            }


                #endregion


                _userRepository.UpdateUser(edit);
            _userRepository.Save();
            return ResultEditAccount.Success;
        }

        public UpdateUserViewModel EditFotUser(int id)
        {
            var user = _userRepository.GetByUserId(id);
            if (user == null)
                return null;
            return new UpdateUserViewModel()
            {
                AvatarName = user.AvatarName,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                IsDelete = false,
                UserName = user.UserName,

            };
        }

        public ResultEditUser EditUser(UpdateUserViewModel model)
        {
            var user = _userRepository.GetByUserId(model.Id);
            if (user == null)
            {
                return ResultEditUser.UserNotFound;
            }

            model.UserName = model.UserName.Trim();
            model.Email = model.Email.ToLower().Trim();
            if (_userRepository.EmailDuplicated(model.UserName, user.Id))
            {
                return ResultEditUser.EmailDuplicated;
            }
            if (_userRepository.UserNameDuplicated(model.UserName, user.Id))
            {
                return ResultEditUser.UserNameDuplicated;
            }
            user.AvatarName = model.AvatarName;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.IsAdmin = model.IsAdmin;
            user.ModifiDate = DateTime.Now;
            user.IsDelete = model.IsDelete;


            #region Manage Image
            if (model.Avatar != null)
            {
                if (user.AvatarName != "1.jpg")
                {
                    string deleteimage = Guid.NewGuid().ToString() +
                        Path.GetExtension(user.AvatarName);
                    if (System.IO.File.Exists(deleteimage))
                        System.IO.File.Delete(deleteimage);
                }
                user.AvatarName = Guid.NewGuid().ToString() +
                   Path.GetExtension(model.Avatar.FileName);

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

        public EditAccountViewModel GetForEditAccount(int Id)
        {
            var account = _userRepository.GetByUserId(Id);
            if (account == null)
                return null;

            return new EditAccountViewModel()
            {
                AvatarName = account.AvatarName,
                Email = account.Email,
                Id = Id,
                Username = account.UserName,

            };
        }

        public UserViewModel GetUserForDetail(int id)
        {
            var user = _userRepository.GetByUserId(id);
            if (user == null)
                return null;
            return new UserViewModel()
            {
                AvatarName = user.AvatarName,
                CreateDate = user.CreateDate,
                Email = user.Email,
                Id = id,
                IsAdmin = user.IsAdmin,
                IsDelete = user.IsDelete,
                ModifiDate = user.ModifiDate,
                UserName = user.UserName,

            };
        }

        public UserViewModel GetUserForShow(int id)
        {
            var user = _userRepository.GetByUserId(id);
            if (user == null)
                return null;
            return new UserViewModel()
            {
                AvatarName = user.AvatarName,
                CreateDate = user.CreateDate,
                Email = user.Email,
                Id = id,
                UserName = user.UserName,

            };
        }

        public User LoginUser(LoginViewModel model)
        {
            var hashpassword = PasswordHelper.EncodePasswordMd5(model.Password);

            var user = _userRepository.GetUserForLogin(model.UserNameOrEmail,
                hashpassword);
            return user;
        }

        public int PageCount()
        {
            return _userRepository.PageCount();
        }

        public ResultRegisterUser RegisterUser(RegisterViewModel model)
        {
            model.Email = model.Email.ToLower().Trim();
            model.UserName = model.UserName.Trim();
            if (_userRepository.EmailExist(model.Email))
            {
                return ResultRegisterUser.EmailNotValid;
            }
            if (_userRepository.UserNameExist(model.UserName))
            {
                return ResultRegisterUser.UserNameNotValid;
            }
            User user = new User()
            {
                UserName = model.UserName,
                CreateDate = DateTime.Now,
                Email = model.Email,
                AvatarName = "1.jpg",
                IsAdmin = false,
                IsDelete = false,
                Password = PasswordHelper.EncodePasswordMd5(model.Password),




            };
            _userRepository.InsertUser(user);
            _userRepository.Save();

            return ResultRegisterUser.Success;
        }
    }
}
