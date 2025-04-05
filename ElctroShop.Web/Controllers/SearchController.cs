using ElctroShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace ElctroShop.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;

        private readonly IProductGroupService _productGroupService;

        public SearchController(ISearchService searchService,
            IProductGroupService productGroupService)
        {
            _searchService = searchService;
            _productGroupService = productGroupService;
        }

        public IActionResult Index(string q)
        {
            var search = _searchService.SearchProduct(q);

            ViewBag.Group = _productGroupService.GetProductGroupForShow();
            return View(search);
        }
    }

}
