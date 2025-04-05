using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElctroShop.Domain.Models.Product;
using ElctroShop.Domain.Models.User;

namespace ElctroShop.Domain.Models.Order
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }

        public bool IsFainaly { get; set; }

        #region Relation
        [ForeignKey("UserId")]
        public ElctroShop.Domain.Models.User.User? User { get; set; }

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
        public ElctroShop.Domain.Models.Product.Product? Product { get; set; }


        #endregion
    }
}
