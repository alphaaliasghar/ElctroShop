using ElectonShop.Domain.Interfaces;
using ElectonShop.Domain.Models.Order;
using ElectonShop.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Infra.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ElectonContext _context;

        public OrderRepository(ElectonContext context)
        {
            _context = context;
        }

        public OrderDetail? CheckOrderDetail(int orderId, int productId)
        {
            return _context.orderDetails
                 .Include(x => x.Product)
               .Where(p => p.OrderId == orderId && p.ProductId == productId && !p.IsDelete)
               .FirstOrDefault();
        }

        public Order? CheckUserOrder(int userId)
        {
            return _context.Orders
                 .Include(o => o.OrderDetails.Where(o => !o.IsDelete))
                 .ThenInclude(o => o.Product)
                 .FirstOrDefault(p => p.UserId == userId && !p.IsDelete && !p.IsFainaly);
        }

        public void DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
        }

        public void DeleteOrderDetail(OrderDetail orderdetail)
        {
            _context.orderDetails.Remove(orderdetail);
        }

        public Order GetCurrentOrder(int userId)
        {
            return _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .AsNoTracking()
                .FirstOrDefault(o => o.UserId == userId && !o.IsDelete && !o.IsFainaly);
        }

        public Order? GetOrderbyId(int orderId)
        {
            return _context.Orders
                 .FirstOrDefault(o => o.Id == orderId && !o.IsFainaly);
        }

        public OrderDetail? GetOrderDetail(int orderdetailId, int productId)
        {
            return _context.orderDetails
                .Include(o => o.Product)
                .Include(o => o.Order)
                .FirstOrDefault(p => p.Id == orderdetailId && p.ProductId == productId);

        }

        public OrderDetail GetOrderDetailById(int orderDetailId, int productId)
        {
            return _context.orderDetails
                .Include(o => o.Product)
                .Include(o => o.Order)
                .FirstOrDefault(o => o.Id == orderDetailId
                && o.ProductId == productId);
        }

        public OrderDetail? GetOrderDetailById(int id)
        {
            return _context.orderDetails
                  .FirstOrDefault(o => o.Id == id);
        }

        public void InsertOrder(Order order)
        {
            _context.Orders.Add(order);
        }

        public void InsertOrderDetail(OrderDetail orderdetail)
        {
            _context.orderDetails.Add(orderdetail);
        }

        public int OrderSum(int orderId)
        {
            return _context.orderDetails
               .Where(p => p.OrderId == orderId && !p.IsDelete)
               .Sum(p => p.Price * p.Count);

        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
        }
        
        public void UpdateOrderDetail(OrderDetail orderdetail)
        {
            _context.orderDetails.Update(orderdetail);
        }

        public int CountShopCart(int userId)
        {

            var order = _context.Orders
                .Include(o => o.OrderDetails)
                .Where(o => o.Id == userId).ToList();
            return order.Sum(od => od.OrderDetails.Sum(d => d.Count));
        }
    }
}
