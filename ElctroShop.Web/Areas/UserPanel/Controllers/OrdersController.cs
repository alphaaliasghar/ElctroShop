using ElctroShop.Application.Services.Implemation;
using ElctroShop.Application.Services.Interfaces;
using ElctroShop.Domain.Models.Order;
using ElctroShop.Domain.Models.Product;
using ElctroShop.Domain.Models.User;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ElctroShop.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        private readonly IProductService _productservice;

        public OrdersController(IOrderService orderService,
            IProductService productservice)
        {
            _orderService = orderService;
            _productservice = productservice;
        }

        public IActionResult Index(int productId)
        {
            int CurrentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var order = _orderService.GetCurrentOrder(CurrentUserId);


            if (order == null || !order.OrderDetails.Any()) // چک کردن لیست آیتم‌ها
            {
                return View("EmptyCart");
            }

            return View(order);
        }


        #region DeleteOrderDetail

        [HttpPost]
        public IActionResult DeleteOrder(int orderdetailId, int productId)
        {
            int CurrentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            bool result = _orderService.DeleteOrderDetail(CurrentUserId, orderdetailId, productId);

            if (!result)
            {
                return NotFound();
            }

            var order = _orderService.GetOrderByUserId(CurrentUserId); // این متد رو باید برای دریافت سفارش کاربر اضافه کنی.

            // اگر هیچ محصولی باقی نمونده باشد
            if (order != null && (order.OrderDetails == null || !order.OrderDetails.Any()))
            {
                // اگر سبد خرید خالی شد، کاربر را به صفحه اصلی سبد خرید هدایت می‌کنیم
                return RedirectToAction("Index", "Orders", new { area = "UserPanel" });
            }

            return RedirectToAction("Index", "Orders", new { area = "UserPanel", orderdetailId = orderdetailId, productId });
        }
        #endregion

        public IActionResult QuantityProduct(int id, string command)
        {
            bool result = _orderService.QuantityProduct(id, command);
            return RedirectToAction("Index");
        }

        public IActionResult CompletePurchase(int orderId)
        {
            var newOrder = _orderService.CompletePurchase(orderId);

            return RedirectToAction("Index", "Orders"
                , new { area = "UserPanel", orderId = newOrder });
        }

    }
}
