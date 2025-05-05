using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Domain.Models.Product
{
    public class ProductGroup : BaseEntity
    {
        [Display(Name = "سر گروه ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string GroupTitle { get; set; }

        [Display(Name = " تصویر  سر گروه ها ")]
        public string? GroupName { get; set; }

        #region Relation
        public List<SubGroup>? SubGroups { get; set; }

        public List<Product>? Products { get; set; }

        #endregion
    }
    public class SubGroup : BaseEntity
    {
        [Display(Name = "زیر گروه ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string SubGroupTitle { get; set; }

        public int GroupId { get; set; }



        #region Relation
        [ForeignKey("GroupId")]
        public ProductGroup? ProductGroup { get; set; }

        public List<Product>? Products { get; set; }
        #endregion

    }
}
