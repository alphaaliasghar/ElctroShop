using ElctroShop.Domain.Enums;
using ElctroShop.Domain.Models.Order;
using ElctroShop.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Application.Services.Interfaces
{
    public interface IOrderService
    {
        int AddOrder(int userId, int productId);

        Order GetCurrentOrder(int userId);

        bool DeleteOrderDetail(int userid, int orderdetailId,int productId);

        Order GetOrderByUserId(int userId);

        OrderDetail? GetOrderDetailById(int id);

        int CompletePurchase(int orderId);

        bool QuantityProduct(int id ,string command);
    }
}
