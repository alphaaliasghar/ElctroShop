using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Domain.Models
{
    public class Comment : BaseEntity
    {
        public int UserId { get; set; }

        public int ProductId { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string UserName { get; set; }

        [Display(Name = "توضیحات ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]


        public string Description { get; set; }

        [ForeignKey("UserId")]
        public ElctroShop.Domain.Models.User.User? User { get; set; }

        [ForeignKey("ProductId")]
        public ElctroShop.Domain.Models.Product. Product? Product { get; set; }

    }
}
