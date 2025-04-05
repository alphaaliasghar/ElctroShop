using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Domain.Models.Product
{
    public class ProductGallery:BaseEntity
    {
        public int ProductId { get; set; }

        public string? ImageName { get; set; }

        #region Relation
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
        #endregion
    }
}
