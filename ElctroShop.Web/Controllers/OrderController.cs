using ElctroShop.Application.Services.Implemation;
using ElctroShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ElctroShop.Web.Controllers
{
    public class OrderController : Controller
    {

        private readonly IOrderService _orderService;
       
        private readonly IProductService _productservice;

        public OrderController(IOrderService orderService
            ,IProductService productService)
        {
            _orderService = orderService;
            _productservice = productService;
        }


        #region AddToCard
        public IActionResult AddToCard(int productId)
        {
           
            int CurrentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var order = _orderService.AddOrder(CurrentUserId, productId);
            return RedirectToAction("Index","Orders", new {area= "UserPanel", productId });
        }
        #endregion

    
    }
}
