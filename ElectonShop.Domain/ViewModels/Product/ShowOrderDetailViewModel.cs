using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Domain.ViewModels.Product
{
    public class ShowOrderDetailViewModel
    {
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [Display(Name = "تصویر")]
        public string? ImageName { get; set; }
        [Display(Name = "تعداد")]
        public int Count { get; set; }

        [Display(Name = "قیمت")]
        public int Price { get; set; }       
    }
}
