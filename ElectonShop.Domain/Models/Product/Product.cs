using ElectonShop.Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Domain.Models.Product
{
    public class Product : BaseEntity
    {
        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        
        public string Title { get; set; }

        [Display(Name = "قیمت محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }

        [Display(Name = "تصویر محصول")]
        public string? ImageName { get; set; }

        [Display(Name = "تگ ها")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Tags { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }

        [Display(Name = "سر گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int GroupId { get; set; }

        [Display(Name = "زیر گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int SubGroupId { get; set; }

        [Display(Name = " موجود است؟")]
        public bool Available { get; set; }


        [Display(Name = " محصول پرطرفدار؟")]
        public bool Popularproduct { get; set; }

        [Display(Name = "  پر فروش ؟")]
        public bool Bestseller { get; set; }

        #region Relation
        [ForeignKey("GroupId")]
        public ProductGroup? ProductGroup { get; set; }

        [ForeignKey("SubGroupId")]
        public SubGroup? SubGroup { get; set; }

        public List<ProductGallery>? ProductGalleries { get; set; }

        public List<OrderDetail>? OrderrDetails { get; set; } 
        #endregion
    }
}
