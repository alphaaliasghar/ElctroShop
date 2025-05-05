using ElctroShop.Web.Areas.UserPanel.Controllers;
using ElectonShop.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ElectonShop.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]

    public class OrderController : UserPanelBaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            int CurrentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var order = _orderService.GetCurrentOrder(CurrentUserId);

            if (order == null || !order.OrderDetails.Any())
            {
                return View("_EmptyCart");
            }

            return View(order);
        }

        #region DeleteOrderDetail
        [HttpPost]
        public IActionResult DeleteOrderDetail(int orderdetailId, int productId)
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
                return RedirectToAction("Index", "Order", new { area = "UserPanel" });
            }

            return RedirectToAction("Index", "Order", new { area = "UserPanel", orderdetailId = orderdetailId, productId });
        }
        #endregion

        #region QuantityProduct
        public IActionResult QuantityProduct(int id, string command)
        {
            bool result=_orderService.QuantityProduct(id, command);
            
            return RedirectToAction("Index");
        }
        #endregion

        #region CompletePurchase
        public IActionResult CompletePurchase(int orderId)
        {
            var newOrder = _orderService.CompletePurchase(orderId);

            return RedirectToAction("Index", "Orders"
                , new { area = "UserPanel", orderId = newOrder });
        }

        #endregion

    }
}
