using ElectonShop.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ElectonShop.Web.Components
{
    public class ProductGroupsViewComponent:ViewComponent
    {
        private readonly IGroupServices _groupServices;

        public ProductGroupsViewComponent(IGroupServices groupServices)
        {
            _groupServices = groupServices;
        }

        public async Task<IViewComponentResult>InvokeAsync()
        {
            var group = _groupServices.ShowGroup();
            return View("_ProductGroup",group);
        }
    }
}
