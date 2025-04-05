using ElctroShop.Application.Services.Interfaces;
using ElctroShop.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElctroShop.Web.Components
{
    public class ProductGroupsViewComponent:ViewComponent
    {
        private readonly IProductGroupService _productGroupService;

        public ProductGroupsViewComponent(IProductGroupService productGroupService)
        {
            _productGroupService = productGroupService;
        }

        public async Task<IViewComponentResult>InvokeAsync()
        {
            var group = _productGroupService.GetProductGroupForShow();
            return View("_ProductGroup", group);
        }
    }
}
