using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Domain.ViewModels.Product
{
    public class ProductSubGroupViewModel
    {
        public int Id { get; set; }
        [Display(Name = "عنوان زیر گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string SubgroupTitle { get; set; }

        public int GroupId { get; set; }
    }

    #region CreateSubgroup
    public class CreateSubgroupViewModel
    {
        [Display(Name = "عنوان زیر گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string SubgroupTitle { get; set; }

        public int GroupId { get; set; }
    }
    #endregion

    #region UpdateSubGroup
    public class UpdateSubgroupViewModel
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        [Display(Name = "عنوان زیر گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string SubgroupTitle { get; set; }
    }
    #endregion

}
