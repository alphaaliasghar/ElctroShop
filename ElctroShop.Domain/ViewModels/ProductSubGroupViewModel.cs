using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Domain.ViewModels
{
    #region SubGroup
    public class SubGroupViewModel
    {
        public int Id { get; set; }


        [Display(Name = "زیر گروه")]
        public string? SubGroupTitle { get; set; }

        public int GroupId { get; set; }

        [Display(Name = "سر گروه")]

        public string? GroupTitle { get; set; }
    }
    #endregion

    #region Create
    public class CreateSubgroupViewModel
    {
        [Display(Name = "زیر گروه")]
        public string? SubGroupTitle { get; set; }

        public int GroupId { get; set; }
    }
    #endregion

    #region Edit
    public class UpdateSubgroupViewModel
    {

        public int Id { get; set; } 

        [Display(Name = "زیر گروه")]
        public string? SubGroupTitle { get; set; }

        public int GroupId { get; set; }
    }
    #endregion

}
