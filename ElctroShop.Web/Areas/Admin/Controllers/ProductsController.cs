using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElctroShop.Domain.Models.Product;
using ElctroShop.Infra.Data.Context;
using ElctroShop.Application.Services.Interfaces;
using ElctroShop.Domain.ViewModels;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Core.Infrastructure;

namespace ElctroShop.Web.Areas.Admin.Controllers
{

    public class ProductsController : AdminBaseController
    {
        private readonly ElctroShopContext _context;

        private readonly IProductService _productServive;

        private readonly ISubGroupService _subgroupServive;

        private readonly IProductGroupService _productgroupServive;

        private readonly IGalleryService _galleryService;

        public ProductsController(ElctroShopContext context,
            IProductService productServive,
            ISubGroupService subgroupServive,
            IProductGroupService productgroupServive,
            IGalleryService galleryService)
        {
            _context = context;
            _productServive = productServive;
            _subgroupServive = subgroupServive;
            _productgroupServive = productgroupServive;
            _galleryService = galleryService;
        }







        #region List
        public IActionResult Index(int pageId = 1)
        {
            int take = 6;
            int pagecount = (int)Math.Ceiling((double)
                _productServive.PageCount() / take);
            int skip = (pageId - 1) * take;
            ViewBag.PageCount = pagecount;
            ViewBag.CurrentPage = pageId;

            var product = _productServive.GetAll(take, skip);
            return View(product);
        }
        #endregion


        #region Details
        public IActionResult Details(int id)
        {
            var result = _productServive.GetForDetails(id);

            if (result == null)
                return NotFound();
            return View(result);
        }
        #endregion


        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            var groups = _productgroupServive.GetSelectedList();

            ViewData["GroupId"] = new SelectList(groups, "Id", "Title");
            ViewData["SubGroupId"] = new SelectList(_subgroupServive.SelectedList(groups.First().Id), "Id", "Title");



            return View();



        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateProductviewModel model)
        {
            var group = _productgroupServive.GetSelectedList();

            #region Validation
            if (!ModelState.IsValid)
            {

                ViewData["GroupId"] = new SelectList(group, "Id", "Title");
                ViewData["SubGroupId"] = new SelectList(_subgroupServive.SelectedList(group.First().Id), "Id", "Title");

                return View(model);
            }
            #endregion


            var result = _productServive.CreateProduct(model);
            switch (result)
            {
                case Domain.Enums.ResultCreateProduct.Success:

                    TempData["SuccessMessage"] = "ثبت با موفقیت انجام شد";
                    return RedirectToAction(nameof(Index));
                case Domain.Enums.ResultCreateProduct.GroupNotValid:

                    TempData["ErrorMessage"] = "ثبت با خطا مواجه  شد";
                    return View(model);
                case Domain.Enums.ResultCreateProduct.SubGroupNotFound:
                    TempData["ErrorMessage"] = "ثبت با خطا مواجه  شد";
                    return View(model);
            }


            ViewData["GroupId"] = new SelectList(group, "Id", "Title", model.GroupId);
            ViewData["SubGroupId"] = new SelectList(_subgroupServive.SelectedList(group.First().Id), "Id", "Title",
                model.SubGroupId);
            return View(model);
        }

        #endregion




        #region Edit

        public IActionResult Edit(int id)
        {

            var product = _productServive.GetForEdit(id);
            if (product == null)
            {
                return NotFound();
            }

            var group = _productgroupServive.GetSelectedList();

            ViewData["GroupId"] = new SelectList(group, "Id", "Title", product.GroupId);
            ViewData["SubGroupId"] = new SelectList(_subgroupServive.SelectedList(product.GroupId), "Id", "Title", product.SubGroupId);


            ViewBag.Galleries = _galleryService.GetProductGalleries(product.Id);

            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditProductViewModel model)
        {
            var group = _productgroupServive.GetSelectedList();

            #region Validation
            if (!ModelState.IsValid)
            {
                ViewData["GroupId"] = new SelectList(group, "Id", "Title", model.GroupId);
                ViewData["SubGroupId"] = new SelectList(_subgroupServive.SelectedList(model.GroupId), "Id", "Title", model.SubGroupId);

                return View(model);
            }
            #endregion



            var result = _productServive.EditProduct(model);
            switch (result)
            {
                case Domain.Enums.ResultEditProduct.Success:

                    TempData["SuccessMessage"] = "ویرایش با موفقیت انجام شد";
                    return RedirectToAction("Index");
                case Domain.Enums.ResultEditProduct.ProductNotFound:
                    TempData["ErrorMessage"] = "محصولی پیدا نشد";
                    break;
                case Domain.Enums.ResultEditProduct.GroupNotFound:
                    TempData["ErrorMessage"] = "سر گروه پیدا نشد";
                    break;
                case Domain.Enums.ResultEditProduct.SubGroupNotFound:
                    TempData["ErrorMessage"] = "زیر گروه پیدا نشد";
                    break;

            }


            ViewData["GroupId"] = new SelectList(group, "Id", "GroupTitle", model.GroupId);
            ViewData["SubGroupId"] = new SelectList(_subgroupServive.SelectedList(model.GroupId), "Id", "SubGroupTitle", model.SubGroupId);
            return View(model);
        }

        #endregion

        #region Delete

        public IActionResult Delete(int id)
        {
            var result = _productServive.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }


        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteConfirmed(int id)
        //{
        //    var result = _productServive.DeleteProduct(id);
        //    return RedirectToAction(nameof(Index));
        //}


        #endregion

        #region GetSubGroup
        public IActionResult GetSubGroup(int groupId)
        {
            ViewBag.SubGroupId = new SelectList(_subgroupServive.SelectedList(groupId), "Id", "Title");
            return PartialView();
        }
        #endregion

        #region DeleteGallery
        public IActionResult DeleteGallery(int id, int productId)
        {
            var result = _galleryService.DeleteGallery(id);

            switch (result)
            {
                case Domain.Enums.ResultDeleteGallery.Success:
                    break;
                case Domain.Enums.ResultDeleteGallery.ProductGalleryNotFount:
                    break;

            }
            return Redirect("/Admin/Products/Edit/" + productId + "#gallries");
        }
        #endregion
    }
}
