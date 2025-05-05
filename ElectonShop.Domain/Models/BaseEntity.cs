using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Domain.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "تاریخ افزودن ")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "تاریخ ویرایش")]

        public DateTime? ModifiDate { get; set; }

        [Display(Name = "حذف شود ؟")]

        public bool IsDelete { get; set; }
    }
}
