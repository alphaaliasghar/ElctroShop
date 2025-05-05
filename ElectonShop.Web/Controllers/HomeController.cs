using System.Diagnostics;
using ElectonShop.Application.Services.Interface;
using ElectonShop.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElectonShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger,
            IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public IActionResult Index(int pageId=1)
        {
            int take = 12;
            int pagecount = (int)Math.Ceiling((double)_productService.PageCount() / take);
            int skip = (pageId - 1) * take;
            
            var product = _productService.GetAll(take,skip);
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
   
    }
}
