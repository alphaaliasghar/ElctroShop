using ElctroShop.Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Domain.Interfaces
{
    public interface IOrderRepository
    { 
        Order? CheckUserOrder(int userId);

        void InsertOrder(Order order);

        void Save();

        OrderDetail? CheckOrderDetail(int orderId, int productId);


        void UpdateOrderDetail(OrderDetail orderdetail);

        void InsertOrderDetail(OrderDetail orderdetail);

        Order? GetOrderbyId(int orderId);

        int Ordersum(int orderId);

        Order GetCurrentOrder(int userId);

        void DeleteOrderDetail(OrderDetail orderdetail);

        OrderDetail GetOrderDetailById(int orderDetailId,int productId);

        void DeleteOrder(Order order);

        OrderDetail? GetOrderDetailById(int id);

        void UpdateOrder(Order order);

        IEnumerable<OrderDetail>GetOrderDetailByOrderId(int orderId);
    }
}
