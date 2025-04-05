using ElctroShop.Domain.Models.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Domain.ViewModels
{

    #region List
    public class ProductGroupViewModel
    {
        public int Id { get; set; }
        [Display(Name = "سر گروه ")]
        public string GroupTitle { get; set; }

        [Display(Name = "تاریخ ثبت")]

        public DateTime CreateDate { get; set; }

        [Display(Name = "تاریخ ویرایش")]

        public DateTime? ModifiDate { get; set; }

        [Display(Name = "  حذف شود؟")]
        public bool IsDelete { get; set; }

        [Display(Name = "زیر گروه  ")]
        public List<ProductSubGroup> SubGroups { get; set; }
    }
    #endregion

    #region Create
    public class CreateProductGroupViewModel
    {
        [Display(Name = "سر گروه ")]
        public string GroupTitle { get; set; }


    }

    #endregion

    #region Edit
    public class UpdateProductGroupViewModel
    {
        public int Id { get; set; }

        [Display(Name = "سر گروه ")]
        public string GroupTitle { get; set; }


        [Display(Name = " حذف شود؟ ")]
        public bool IsDelete { get; set; }

        [Display(Name = "تاریخ ویرایش")]

        public DateTime? ModifiDate { get; set; }




    }


    #endregion

 
}
