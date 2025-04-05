using ElctroShop.Application.Services.Interfaces;
using ElctroShop.Domain.Interfaces;
using ElctroShop.Domain.Models.Order;
using ElctroShop.Domain.Models.User;
using ElctroShop.Domain.ViewModels;
using ElctroShop.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Application.Services.Implemation
{

    public class OrderService : IOrderService
    {

        private readonly IOrderRepository _orderRepository;

        private readonly IProductService _productService;



        public OrderService(IOrderRepository orderRepository,
            IProductService productService)
        {
            _orderRepository = orderRepository;
            _productService = productService;

        }

        public int AddOrder(int userId, int productId)
        {

            var product = _productService.GetProductbyId(productId);
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

            var orderDetail = _orderRepository.CheckOrderDetail(order.Id, productId);

            if (orderDetail == null)
            {
                orderDetail = new OrderDetail()
                {
                    CreateDate = DateTime.Now,
                    Count = 1,
                    OrderId = order.Id,
                    Price = product.Price,
                    ProductId = productId,
                    IsDelete = false,

                };

                _orderRepository.InsertOrderDetail(orderDetail);

            }
            else
            {
                orderDetail.Count += 1;

                _orderRepository.UpdateOrderDetail(orderDetail);
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

                // سفارش جدید بدون کپی کردن محصولات قبلی
                var newOrder = new Order()
                {
                    UserId = order.UserId,
                    CreateDate = DateTime.Now,
                    IsDelete = false,
                    IsFainaly = false,
                };

                _orderRepository.InsertOrder(newOrder);
                _orderRepository.Save();

                return newOrder.Id;
            }
            return 0;


        }



        public bool DeleteOrderDetail(int userid, int orderdetailId, int productId)
        {

            var detail = _orderRepository.GetOrderDetailById(orderdetailId, productId);


            if (detail == null)
            {

                return false;
            }




            if (detail.Order == null || detail.Order.UserId != userid)
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
            var checkOrder = _orderRepository.GetOrderbyId(order.Id);
            if (checkOrder != null && (checkOrder.OrderDetails == null || !checkOrder.OrderDetails.Any()))
            {
                _orderRepository.DeleteOrder(checkOrder);
                _orderRepository.Save();
            }

            _orderRepository.Save();
            return true;
        }

        public Order GetCurrentOrder(int userId)
        {
            return _orderRepository.GetCurrentOrder(userId);
        }

        public Order GetOrderByUserId(int userId)
        {
            return _orderRepository.CheckUserOrder(userId);
        }

        public OrderDetail? GetOrderDetailById(int id)
        {
            return _orderRepository.GetOrderDetailById(id);
        }


        public bool QuantityProduct(int id, string command)
        {
            var orderdetail = _orderRepository.GetOrderDetailById(id);

            if (orderdetail == null)
                return false;

            if (command == "up")
            {
                orderdetail.Count += 1;
                _orderRepository.UpdateOrderDetail(orderdetail);
                _orderRepository.Save();
            }
            else if (command == "down")
            {
                if (orderdetail.Count > 1)
                {
                    orderdetail.Count -= 1;
                    _orderRepository.UpdateOrderDetail(orderdetail);
                    _orderRepository.Save();
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
