using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Domain.ViewModels
{
    #region ListUser
    public class UserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Email { get; set; }

        [Display(Name = "تصویر پروفایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? AvatarName { get; set; }

        [Display(Name = "تاریخ ثبت ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public DateTime CreateDate { get; set; }

        [Display(Name = "تاریخ ویرایش")]


        public DateTime? ModifiDate { get; set; }

        [Display(Name = "ادمین؟")]


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

        [Display(Name = "تصویر  کاربر")]
        public string? AvatarName { get; set; }

        [Display(Name = " تصویر")]
        public IFormFile? Avatar { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = " ادمین؟")]
        public bool IsAdmin { get; set; }

        [Display(Name = " حذف؟")]
        public bool IsDelete { get; set; }
    }
    #endregion

    #region EditUser
    public class EditUserViewModel
    {
        public int Id { get; set; } 

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Email { get; set; }

        [Display(Name = "تصویر  کاربر")]
        public string? AvatarName { get; set; }

        [Display(Name = " تصویر")]
        public IFormFile? Avatar { get; set; }

        [Display(Name = "کلمه عبور جدید")]
       
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [Display(Name = " ادمین؟")]
        public bool IsAdmin { get; set; }

        [Display(Name = " حذف؟")]
        public bool IsDelete { get; set; }


    }
    #endregion

}
