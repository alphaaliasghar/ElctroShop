using ElectonShop.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace ElectonShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        private readonly IGroupServices _groupService;

        private readonly ISubGroupService _subGroupService;

        public ProductController(IProductService productService,
            IGroupServices groupService,
            ISubGroupService subGroupService)
        {
            _productService = productService;
            _groupService = groupService;
            _subGroupService = subGroupService;
        }
        #region ShowProduct

        [Route("product/showproduct/{productId}")]
        public IActionResult ShowProduct(int productId)
        {
            var product = _productService.GetForDetails(productId);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        #endregion

        #region showGroup
        [Route("showgroup/{groupId}/{groupTitle}")]
        public IActionResult ShowGroup(int groupId, string groupTitle)
        {
            var product = _productService.GetGroupById(groupId);
            return View(product);
        }
        #endregion

        #region ShowSubGroup
        [Route("showsubgroup/{SubGroupId}/{subgroupTitle}")]
        public IActionResult ShowSubGroup(int SubGroupId, string subgroupTitle)
        {
            var subgroup = _productService.GetSubGroupById(SubGroupId);
            return View(subgroup);
        }
        #endregion

        #region ShowPopularProduct
        public IActionResult Popularproduct()
        {
            var result = _productService.GetAllPopularproduct();
            return View(result);
        }
        #endregion

        #region ShowBestseller
        public IActionResult Bestller()
        {
            var bestller = _productService.GetAllBestseller();
            return View(bestller);
        }
        #endregion
    }
}
