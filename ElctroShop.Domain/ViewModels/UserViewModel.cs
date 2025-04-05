using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Domain.ViewModels
{
    #region ListUser
    public class UserViewModel
    {
        public int Id { get; set; }
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Display(Name = "تصویر کاربر")]

        public string AvatarName { get; set; }

        [Display(Name = "تاریخ ثبت نام")]

        public DateTime CreateDate { get; set; }

        [Display(Name = "تاریخ ویرایش")]
        public DateTime? ModifiDate { get; set; }

        [Display(Name = " ادمین است؟")]
        public bool IsAdmin { get; set; }

        [Display(Name = "حذف شده؟")]
        public bool IsDelete { get; set; }
    }
    #endregion

    #region CreateUser
    public class CreateUserViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Password { get; set; }

        [Display(Name = "تصویر کاربر")]

        public IFormFile? Avatar { get; set; }




        [Display(Name = " ادمین است؟")]
        public bool IsAdmin { get; set; }
    }
    #endregion

    #region EditUser
    public class UpdateUserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور جدید")]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [Display(Name = "تصویر کاربر")]

        public IFormFile? Avatar { get; set; }

        public string? AvatarName { get; set; }

        [Display(Name = " ادمین است؟")]
        public bool IsAdmin { get; set; }

        [Display(Name = " حذف شود؟")]
        public bool IsDelete { get; set; }
    }
    #endregion

    #region ChangePassword
    public class ChangePasswordViewModel
    {
        [Display(Name = "کلمه عبور فعلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; }
    }
    #endregion
}
