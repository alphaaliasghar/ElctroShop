using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Domain.Models.User
{
    public class User : BaseEntity
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage ="اطفا ایمیل صحیح را وارد کنید")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تصویر پروفایل")]
        public string? AvatarName { get; set; }

        [Display(Name = "ادمین ؟")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public bool IsAdmin { get; set; }
    }
}
