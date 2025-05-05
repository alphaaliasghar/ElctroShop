using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Domain.ViewModels.Product
{
    public class GalleryViewModel
    {
        public int Id { get; set; }
        [Display(Name = "    شناسه محصول")]
        public int ProductId { get; set; }
        [Display(Name = "    نام تصویر")]
        public string? ImageName { get; set; }

        [Display(Name = "   تاریخ ثبت")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "   تاریخ ویرایش")]
        public DateTime? ModifiDate { get; set; }
    }
}
