using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Domain.ViewModels
{
    #region Information
    public class InformationViewModel
    {
        public int Id { get; set; }

        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Display(Name = "تصویر پروفایل")]

        public string? AvatarName { get; set; }

        [Display(Name = "تاریخ عضویت")]

        public DateTime CreateDate { get; set; }

        [Display(Name = "تاریخ ویرایش")]
        public DateTime? ModifiDate { get; set; }
    }
    #endregion

    #region ChangePassword
    public class ChangePaswordViewModel
    {
        [Display(Name = "کلمه عبور فعلی ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string OldPasword { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string Repassword { get; set; }

    }
    #endregion

    #region EditInformation
    public class EditInformationViewModel
    {
        public int Id { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string Email { get; set; }

        [Display(Name = "تصویر کاربر")]
        public string? AvatarName { get; set; }

        [Display(Name = "تصویر")]
        public IFormFile? Avatar { get; set; }

        [Display(Name = "تاریخ ویرایش")]
        public DateTime? Modifidate { get; set; }
    }
    #endregion

}
