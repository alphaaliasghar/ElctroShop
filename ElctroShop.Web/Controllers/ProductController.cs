using ElctroShop.Application.Services.Interfaces;
using ElctroShop.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ElctroShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
          
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }






        #region ShowProduct

        [Route("/product/ShowProduct/{id}")]
        public IActionResult ShowProduct(int id)
        {
            var product = _productService.GetForDetails(id);

          
          

          

            if (product == null)
                return NotFound();

            _productService.VisitProduct(product.Id);

            return View(product);
        }
        #endregion

        #region ShowGroup
        [Route("/showgroup/{groupId}/{groupTitle}")]
        public IActionResult ShowGroup(int groupId,string groupTitle)
        {
          var group=_productService.GetGroupById(groupId);
           
            return View(group);
        }
        #endregion

        #region ShowSubGroup
        [Route("/showsubgroup/{subgroupId}/{grosubgroupTitleupTitle}")]
        public IActionResult ShowSubGroup(int subgroupId,string grosubgroupTitleupTitle)
        {
            var subgroup=_productService.GetSubGroupById(subgroupId);
            return View(subgroup);
        }
        #endregion


  
    }
}
