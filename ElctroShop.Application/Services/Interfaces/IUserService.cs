using ElctroShop.Domain.Enums;
using ElctroShop.Domain.Models.User;
using ElctroShop.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Application.Services.Interfaces
{
    public interface IUserService
    {
        ResultRegisterUser RegisterUser(RegisterViewModel model);

        User LoginUser(LoginViewModel model);

        int PageCount();

        List<UserViewModel> GetAll(int take, int skip);

        ResultCreateUser CreateUser(CreateUserViewModel model);

        UserViewModel GetUserForDetail(int id);

        UpdateUserViewModel EditFotUser(int id);

        ResultEditUser EditUser(UpdateUserViewModel model);

        bool DeleteUser(int id);
        ResultChanePassword ChanePassword(int userid, ChangePasswordViewModel model);

        UserViewModel GetUserForShow(int id);

        EditAccountViewModel GetForEditAccount(int Id);

        ResultEditAccount EditAccount(EditAccountViewModel model);


    }
}
