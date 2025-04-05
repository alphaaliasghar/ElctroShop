using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Domain.Models.Product
{
    public class ProductGroup:BaseEntity
    {
        [Display(Name = "عنوان سر گروه ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string GroupTitle { get; set; }

        public List<ProductSubGroup>? SubGroups { get; set; } 

    }
    public class ProductSubGroup:BaseEntity
    {
        [Display(Name = "عنوان زیر گروه ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string SubGroupTitle { get; set; }

        public int GroupId { get; set; }

        [ForeignKey("GroupId")]
        public ProductGroup? Group { get; set; } 


    }
}
