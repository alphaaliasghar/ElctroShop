using ElctroShop.Domain.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Domain.ViewModels
{
    public class ShowGroupViewModel
    {
        public int Id { get; set; }

        public string GroupTitle { get; set; }

        public List<ProductSubGroup>? SubGroups { get; set; }
    }

    public class ShowSubGroupViewModel
    {
        public int Id { get; set; }

        public string? SubGroupName { get; set; } 
    }
    
}
