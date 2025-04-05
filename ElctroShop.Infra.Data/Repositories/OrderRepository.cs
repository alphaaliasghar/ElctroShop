using ElctroShop.Domain.Interfaces;
using ElctroShop.Domain.Models.Order;
using ElctroShop.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Infra.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ElctroShopContext _context;

        public OrderRepository(ElctroShopContext context)
        {
            _context = context;
        }

        public OrderDetail CheckOrderDetail(int orderId, int productId)
        {
            return _context.OrderDetails.
                Include(d => d.Product).
                  Where(d => d.OrderId == orderId && d.ProductId == productId
                  && !d.IsDelete)
                  .FirstOrDefault();
        }

        public Order? CheckUserOrder(int userId)
        {
            var order = _context.Orders
                .Include(o => o.OrderDetails.Where(o=>!o.IsDelete))
                .ThenInclude(od => od.Product)
                .FirstOrDefault(o => o.UserId == userId && !o.IsDelete && !o.IsFainaly);
            return order;
        }

        public void DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
        }

        public void DeleteOrderDetail(OrderDetail orderdetail)
        {
            _context.OrderDetails.Remove(orderdetail);
        }

        public Order GetCurrentOrder(int userId)
        {
            return _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .AsNoTracking()
                .FirstOrDefault(o => o.UserId == userId && !o.IsDelete&&!o.IsFainaly);
        }

        public Order? GetOrderById(int userId)
        {
            return _context.Orders
        .Include(o => o.OrderDetails.Where(od => !od.IsDelete))
        .ThenInclude(od => od.Product)
        .SingleOrDefault(o => o.UserId == userId && !o.IsDelete && !o.IsFainaly);
        }

        public Order? GetOrderbyId(int orderId)
        {
            return _context.Orders
                .Include(o => o.OrderDetails)
               .SingleOrDefault(o => o.Id == orderId);
        }



        public OrderDetail GetOrderDetailById(int orderDetailId, int productId)
        {
            return _context.OrderDetails
                .Include(o => o.Product)
                .Include(o => o.Order)
                .FirstOrDefault(o => o.Id == orderDetailId
                && o.ProductId == productId);
        }

        public OrderDetail? GetOrderDetailById(int id)
        {
            return _context.OrderDetails
                  .FirstOrDefault(o => o.Id == id);
        }

        public IEnumerable<OrderDetail> GetOrderDetailByOrderId(int orderId)
        {
           return _context.OrderDetails
                .Where(d=>d.OrderId==orderId
                &&!d.IsDelete).Include(d=>d.Product).ToList();
        }

        public void InsertOrder(Order order)
        {
            _context.Orders.Add(order);
        }

        public void InsertOrderDetail(OrderDetail orderdetail)
        {
            _context.OrderDetails.Add(orderdetail);
        }

        public int Ordersum(int orderId)
        {
            return _context.OrderDetails
                 .Where(o => o.OrderId == orderId && !o.IsDelete)
                 .Sum(o => o.Price);
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
            _context.OrderDetails.Update(orderdetail);
        }
    }
}
