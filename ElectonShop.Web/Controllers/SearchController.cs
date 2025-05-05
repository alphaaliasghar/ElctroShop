using ElectonShop.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ElectonShop.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public IActionResult Search(string q)
        {
            var search = _searchService.Search(q);
            return View(search);
        }
    }
}
