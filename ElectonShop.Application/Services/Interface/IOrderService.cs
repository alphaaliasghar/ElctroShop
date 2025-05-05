using ElectonShop.Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Application.Services.Interface
{
    public interface IOrderService
    {
        int AddOrder(int userId, int productId);

        int CompletePurchase(int orderId);

        bool DeleteOrderDetail(int userId, int orderdetailId, int productId);

        Order GetCurrentOrder(int UserId);

        Order GetOrderByUserId(int userId);

        OrderDetail? GetOrderDetailById(int id);

        bool QuantityProduct(int id, string command);

        int CountShopCart(int id);
    }
}
