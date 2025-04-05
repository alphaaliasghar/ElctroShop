using ElctroShop.Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Domain.ViewModels
{
    #region Order
    public class OrderViewModel
    {
        public int Id { get; set; }

        public Order? Order { get; set; }

        public List<OrderDetail>? OrderDetails { get; set; }
    }
    #endregion

    #region Create
    public class CreateOrderViewModel
    {
        public int ProductId { get; set; }

        public int Count { get; set; } 
        #endregion
    }
}
