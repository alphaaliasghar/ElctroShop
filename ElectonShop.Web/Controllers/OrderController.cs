using ElectonShop.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ElectonShop.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult AddToCart(int productId)
        {
            int CurrentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = _orderService.AddOrder(CurrentUserId, productId);
            return RedirectToAction("Index", "Order", new { area = "UserPanel", productId });
        }

        #region CountShopCart
        public IActionResult CountShopCart()
        {
            int CurrentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var count = _orderService.CountShopCart(CurrentUserId);
            return Json(count);
        }
        #endregion
    }
}

