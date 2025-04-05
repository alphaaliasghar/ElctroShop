using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Domain.ViewModels
{
    #region Register
    public class RegisterViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "لطفا ایمیل صحیح را وارد کنید")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; }

        [Display(Name = "قوانین سایت را میپذیرم!")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]

        public bool Rouls { get; set; }
    }
    #endregion

    #region Login
    public class LoginViewModel
    {
        [Display(Name = "نام کاربری یا ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string UserNameOrEmail { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = " مرا به خاطر بسپار !")]
        public bool RememberMy { get; set; }
    }
    #endregion

    #region EditAccount
    public class EditAccountViewModel
    {
        public int Id { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Username { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage ="لطفا ایمیل صحیح را وارد کنید")]
        public string Email { get; set; }

        [Display(Name = "تصویر")]
        public string? AvatarName { get; set; }

        [Display(Name = "تصویر")]
        public IFormFile? Avatar { get; set; }
    }
    #endregion
}
