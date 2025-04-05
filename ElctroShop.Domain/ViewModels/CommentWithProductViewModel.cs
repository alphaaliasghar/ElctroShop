using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Domain.ViewModels
{
    public class CommentWithProductViewModel
    {
        public ProductViewModel Product { get; set; } 

        public CommentViewModel Comment { get; set; }

        public List<CommentViewModel> Comments { get; set; }
    }
}
