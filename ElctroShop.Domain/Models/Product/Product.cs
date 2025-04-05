using ElctroShop.Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Domain.Models.Product
{
    public class Product : BaseEntity
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
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string? ImageName { get; set; }

        [Display(Name = "بازدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public int Visit { get; set; }

        [Display(Name = "تگ ها")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string Tags { get; set; }

        [Display(Name = "سر گروه")]
        public int GroupId { get; set; }


        [Display(Name = "زیر گروه")]
        public int SubGroupId { get; set; }


        #region Relation
        [ForeignKey("GroupId")]
        public ProductGroup? ProductGroup { get; set; }

        [ForeignKey("SubGroupId")]
        public ProductSubGroup? ProductSubGroup { get; set; }
       
        public List<ProductGallery>? ProductGalleries { get; set; }

        public List<OrderDetail>? OrderDetails { get; set; }

        public List<Comment>? Comments { get; set; } 

        #endregion
    }


}
