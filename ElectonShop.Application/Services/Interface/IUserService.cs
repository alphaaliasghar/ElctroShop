using ElectonShop.Domain.Enums;
using ElectonShop.Domain.Models.User;
using ElectonShop.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Application.Services.Interface
{
    public interface IUserService
    {
        ResultRegisterUser RegisterUser(RegisterViewModel model);

        User LoginUser(LoginViewModel model);

        InformationViewModel GetInformationForShow(int id);

        ResultChangePassword ChangePassword(int userId, ChangePaswordViewModel model);

        List<UserViewModel> GetAll(int take, int skip);

        int CountPage();

        UserViewModel GetForDetail(int id);

        EditInformationViewModel GetForEdit(int id);

        ResultEditInformation EditInformation(EditInformationViewModel model);


        ResultCreateUser CreateUser(CreateUserViewModel model);

        EditUserViewModel GetForEditUser(int id);

        ResultEditUser EditUser(EditUserViewModel model);

        bool Delete(int id);
    }
}
