using ElectonShop.Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Domain.Interfaces
{
    public interface IOrderRepository
    {
        void InsertOrder(Order order);

        void InsertOrderDetail(OrderDetail orderdetail);

        void UpdateOrder(Order order);

        void UpdateOrderDetail(OrderDetail orderdetail);

        void DeleteOrder(Order order);

        void DeleteOrderDetail(OrderDetail orderdetail);

        void Save();

        Order? GetOrderbyId(int orderId);
         
        OrderDetail? GetOrderDetail(int orderdetailId, int productId);

        int OrderSum(int orderId);

        OrderDetail GetOrderDetailById(int orderDetailId, int productId);

        OrderDetail? CheckOrderDetail(int orderId,int productId);

        Order? CheckUserOrder(int userId);


        Order GetCurrentOrder(int userId);

        OrderDetail? GetOrderDetailById(int id);

        int CountShopCart(int userId);
    }
}
