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
    #region Product
    public class ShowProductViewModel
    {
        public int Id { get; set; }
        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string Title { get; set; }

        [Display(Name = "قیمت محصول")]

        public int Price { get; set; }

        [Display(Name = "تصویر محصول")]
        public string? ImageName { get; set; }

        [Display(Name = "تگ ها")]

        public string Tags { get; set; }

        [Display(Name = "توضیحات")]

        public string Description { get; set; }

        [Display(Name = "سر گروه")]

        public ProductGroup? Group { get; set; }

        [Display(Name = "زیر گروه")]

        public SubGroup? SubGroup { get; set; }

        [Display(Name = " موجود است؟")]
        public bool Available { get; set; }


        [Display(Name = " محصول پرطرفدار؟")]
        public bool Popularproduct { get; set; }

        [Display(Name = "  حذف شود؟")]
        public bool IsDelete { get; set; }

        [Display(Name = "  پر فروش ؟")]
        public bool Bestseller { get; set; }

        [Display(Name = "  گالری تصاویر")]
        public List<ProductGallery> ProductGalleries { get; set; }

        [Display(Name = "   تاریخ ثبت")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "   تاریخ ویرایش")]
        public DateTime? ModifiDate { get; set; }
    }
    #endregion

    #region Create
    public class CreateProductViewModel
    {
        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public int Price { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string Description { get; set; }


        [Display(Name = "تصویر")]
        public IFormFile? Image { get; set; }




        [Display(Name = "گالری")]
        public IFormFile[]? Galleries { get; set; }

        public string Tags { get; set; }


        [Display(Name = "  سر گروه")]
        public int GroupId { get; set; }


        [Display(Name = "زیر گروه")]
        public int SubGroupId { get; set; }


        [Display(Name = " موجود است؟")]
        public bool Available { get; set; }


        [Display(Name = " محصول پرطرفدار؟")]
        public bool Popularproduct { get; set; }

        [Display(Name = "  پر فروش ؟")]
        public bool Bestseller { get; set; }
    }
    #endregion

    #region Update
    public class EditProductViewModel
    {
        public int Id { get; set; }

        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public int Price { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string Description { get; set; }

        [Display(Name = "نام تصویر")]
        public string? ImageName { get; set; }

        [Display(Name = "تصویر")]
        public IFormFile? Image { get; set; }

        [Display(Name = "نام گالری")]
        public string? GalleryName { get; set; }

        [Display(Name = "گالری")]
        public IFormFile[]? Galleries { get; set; }

        public string Tags { get; set; }


        [Display(Name = "  سر گروه")]
        public int GroupId { get; set; }


        [Display(Name = "زیر گروه")]
        public int SubGroupId { get; set; }


        [Display(Name = " موجود است؟")]
        public bool Available { get; set; }


        [Display(Name = " محصول پرطرفدار؟")]
        public bool Popularproduct { get; set; }

        [Display(Name = "  پر فروش ؟")]
        public bool Bestseller { get; set; }

        [Display(Name = "    حذف ؟")]
        public bool IsDelete { get; set; }
    }
    #endregion
}
