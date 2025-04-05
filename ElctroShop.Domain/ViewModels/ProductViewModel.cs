using ElctroShop.Domain.Models;
using ElctroShop.Domain.Models.Product;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Domain.ViewModels
{
    #region Product 
    public class ProductViewModel
    {

        public int Id { get; set; } 

        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string ShortDescription { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string Description { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public int Price { get; set; }

        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string? ImageName { get; set; }

        [Display(Name = "بازدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public int Visit { get; set; }

        [Display(Name = "تگ ها")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string Tags { get; set; }

        [Display(Name = " تاریخ ثبت")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "  تاریخ ویرایش")]
        public DateTime? ModifiDate { get; set; }

        [Display(Name = "   حذف شده؟")]
        public bool IsDelete { get; set; }

        [Display(Name = "سر گروه")]
        public ProductGroup? Group { get; set; }


        [Display(Name = "زیر گروه")]
        public ProductSubGroup? SubGroup { get; set; }


        [Display(Name = " گالری ")]
        public List<ProductGallery>? ProductGalleries { get; set; }

        public int? GroupId { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

    }
    #endregion

    #region Create
    public class CreateProductviewModel
    {
        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string ShortDescription { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string Description { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public int Price { get; set; }

        [Display(Name = "تصویر")]
      
        public string? ImageName { get; set; }

        [Display(Name = "تصویر")]
        public IFormFile? Image { get; set; }

        [Display(Name = "گالری محصول")]
        public string? ImageGallery { get; set; }

        [Display(Name = "گالری")]
        public IFormFile[]? Gallery { get; set; }

        [Display(Name = "تگ ها")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Tags { get; set; }

        [Display(Name = "گروه کالا")]
        public int GroupId { get; set; }

        [Display(Name = "زیر گروه")]
        public int SubGroupId { get; set; }
    }
    #endregion

    #region Edit
    public class EditProductViewModel
    {

        public int Id { get; set; } 

        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string ShortDescription { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string Description { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public int Price { get; set; }

        [Display(Name = "تصویر")]

        public string? ImageName { get; set; }

        [Display(Name = "تصویر")]
        public IFormFile? Image { get; set; }

        [Display(Name = "گالری محصول")]
        public string? ImageGallery { get; set; }

        public List<ProductGallery>? Galleries { get; set; }

        [Display(Name = "گالری")]
        public IFormFile[]? Gallery { get; set; }

        [Display(Name = "تگ ها")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Tags { get; set; }

        [Display(Name = "گروه کالا")]
        public int GroupId { get; set; }

        [Display(Name = "زیر گروه")]
        public int SubGroupId { get; set; }
        [Display(Name = " حذف شود؟")]
        public bool IsDelete { get; set; } 
    }
    #endregion
}
