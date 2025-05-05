using ElectonShop.Application.Services.Interface;
using ElectonShop.Domain.Interfaces;
using ElectonShop.Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Application.Services.Implemation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository,
            IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public int AddOrder(int userId, int productId)
        {
            var product = _productRepository.GetProductById(productId);
            if (product == null)
            {
                return 0;
            }
            var order = _orderRepository.CheckUserOrder(userId);
            if (order == null || order.IsFainaly)
            {
                order = new Order()
                {
                    CreateDate = DateTime.Now,
                    IsDelete = false,
                    IsFainaly = false,
                    UserId = userId,
                };
                _orderRepository.InsertOrder(order);
                _orderRepository.Save();
            }
            var orderdetail = _orderRepository.CheckOrderDetail(order.Id, productId);
            if (orderdetail == null)
            {
                orderdetail = new OrderDetail()
                {
                    Count = 1,
                    CreateDate = DateTime.Now,
                    IsDelete = false,
                    OrderId = order.Id,
                    Price = product.Price,
                    ProductId = productId

                };
                _orderRepository.InsertOrderDetail(orderdetail);
                _orderRepository.Save();
            }
            else
            {
                orderdetail.Count++;
                _orderRepository.UpdateOrderDetail(orderdetail);

            }
            _orderRepository.Save();

            return order.Id;
        }

        public int CompletePurchase(int orderId)
        {
            var order = _orderRepository.GetOrderbyId(orderId);
            if (order != null)
            {
                order.IsFainaly = true;
                _orderRepository.UpdateOrder(order);
                _orderRepository.Save();
            }
            else
            {
                var neworder = new Order()
                {
                    CreateDate = DateTime.Now,
                    IsDelete = false,
                    IsFainaly = false,
                    UserId = order.UserId,

                };
                _orderRepository.InsertOrder(neworder);
                _orderRepository.Save();
                return neworder.Id;
            }
            return 0;

        }

        public int CountShopCart(int id)
        {
            return _orderRepository.CountShopCart(id);
        }

        public bool DeleteOrderDetail(int userId, int orderdetailId, int productId)
        {
            var detail = _orderRepository.GetOrderDetailById(orderdetailId, productId);

            if (detail == null)

                return false;

            if (detail.Order == null || detail.Order.UserId != userId)
            {
                return false;
            }

            var order = detail.Order;

            if (detail.Count > 1)
            {
                detail.Count -= 1;
                _orderRepository.UpdateOrderDetail(detail);
            }
            else
            {
                _orderRepository.DeleteOrderDetail(detail);
            }
            var checkorder = _orderRepository.GetOrderbyId(order.Id);
            if (checkorder != null && (checkorder.OrderDetails == null && !checkorder.OrderDetails.Any()))
            {
                _orderRepository.DeleteOrder(checkorder);
                _orderRepository.Save();
            }
            _orderRepository.Save();

            return true;
        }

        public Order GetCurrentOrder(int UserId)
        {
            return _orderRepository.GetCurrentOrder(UserId);
        }

        public Order GetOrderByUserId(int userId)
        {
            return _orderRepository.GetOrderbyId(userId);
        }

        public OrderDetail? GetOrderDetailById(int id)
        {
            return _orderRepository.GetOrderDetailById(id);
        }

        public bool QuantityProduct(int id, string command)
        {
            var orderdetail = _orderRepository.GetOrderDetailById(id);
            if (orderdetail == null)
            {
                
                return false;
            }

           

            if (command == "up")
            {
                orderdetail.Count += 1;
                _orderRepository.UpdateOrderDetail(orderdetail);
            
            }
            else if (command == "down")
            {
                if (orderdetail.Count > 1)
                {
                    orderdetail.Count -= 1;
                    _orderRepository.UpdateOrderDetail(orderdetail);
                
                }
                else
                {
                    _orderRepository.DeleteOrderDetail(orderdetail);
                }
            }
            _orderRepository.Save();
            return true;
        }
    }

}

