using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ElctroShop.Web.Models;
using ElctroShop.Application.Services.Interfaces;
using ElctroShop.Infra.Data.Context;

namespace ElctroShop.Web.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _productService;

    private readonly ElctroShopContext _context;

    public HomeController(IProductService productService,
        ElctroShopContext context)
    {
        _productService = productService;
        _context = context;
    }

    public IActionResult Index(int pageId = 1)
    {


        int take = 8;
        int PageCount = (int)Math.Ceiling((double)_productService.PageCount() / take);
        int skip = (pageId - 1) * take;

        ViewBag.PageCount = PageCount;
        ViewBag.CurrentPage = pageId;

        var product = _productService.GetAll(take, skip);

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
