using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Domain.ViewModels
{
    #region Register
    public class RegisterViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "اطفا ایمیل صحیح را وارد کنید")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه عبور و تکرار کلمه عبور یکسان  نیست ")]
        public string RePassword { get; set; }

        [Display(Name = "  قوانین سایت را میپذیرم!")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public bool Roulse { get; set; }
    }
    #endregion

    #region Login
    public class LoginViewModel
    {
        [Display(Name = "ایمیل یا نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
       
        public string EmailOrUserName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = " مرا به خاطر بسپار ")]
        public bool RememberMy { get; set; }

     
    
    }
    #endregion
}
