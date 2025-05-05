using ElectonShop.Domain.Models.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Domain.Models.Order
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }

        public bool IsFainaly { get; set; }

        #region Relation
        [ForeignKey("UserId")]
        public User.User? User { get; set; }

        public List<OrderDetail>? OrderDetails { get; set; }
        #endregion
    }


    public class OrderDetail : BaseEntity
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Count { get; set; }

        public int Price { get; set; }


        #region Relation

        [ForeignKey("OrderId")]
        public Order? Order { get; set; }
        [ForeignKey("ProductId")]
        public Product.Product? Product { get; set; }
        #endregion
    }
}
