using ElectonShop.Domain.Models.Product;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Domain.ViewModels.Product
{
    #region ProductGroup
    public class ProductGroupViewModel
    {
        public int Id { get; set; }

        [Display(Name = "سر گروه ")]
        public string GroupTitle { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "تاریخ ویرایش")]
        public DateTime? ModifiDate { get; set; }
        [Display(Name = " تصویر گروه ها")]
        public string? GroupName { get; set; }

        [Display(Name = "زیر گروه")]
        public List<SubGroup>? SubGroups { get; set; }

        [Display(Name = " حذف شده؟")]
        public bool IsDelete { get; set; }
    }
    #endregion

    #region CreateGroup
    public class CreateProductGroupViewModel
    {
        [Display(Name = "نام سر گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string GroupTitle { get; set; }

        [Display(Name = "  نام تصویر")]


        public string? GroupName { get; set; }

        [Display(Name = "  تصویر")]


        public IFormFile? Image { get; set; }
    }
    #endregion

    #region EditGroup

    public class UpdateProductGroupViewModel
    {
        public int Id { get; set; }
        [Display(Name = "عنوان سر گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string GroupTitle { get; set; }

        [Display(Name = "  نام تصویر")]
        public string? ImageName { get; set; }

        [Display(Name = "  تصویر")]
        public IFormFile? Image { get; set; }

        [Display(Name = "  حذف شود؟")]
        public bool IsDelete { get; set; }
    }
    #endregion

    #region GetProductGroup
    public class ProductGroupForShow
    {
        public int Id { get; set; }

        public string GroupTitle { get; set; } 

        public string? ImageName { get; set; } 

        public List<SubGroup>? SubGroups { get; set; }
    }
    #endregion
}
