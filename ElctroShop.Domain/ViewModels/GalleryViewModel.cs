using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Domain.ViewModels
{
    public class GalleryViewModel
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Display(Name = "شناسه محصول")]
        public int ProductId { get; set; }

        [Display(Name = "نام تصویر")]
        public string? ImageName { get; set; }

 

        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "تاریخ ویرایش")]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "حذف شده ؟")]
        public bool IsDelete { get; set; }

    }
}
