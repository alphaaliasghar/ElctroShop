using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElectonShop.Domain.Models.Product;
using ElectonShop.Infra.Data.Context;
using ElectonShop.Application.Services.Interface;
using System.Security.AccessControl;
using ElectonShop.Domain.ViewModels.Product;

namespace ElectonShop.Web.Areas.Admin.Controllers
{

    public class ProductsController : AdminBaseController
    {
        private readonly ElectonContext _context;

        private readonly IProductService _productService;

        private readonly IGroupServices _groupServices;

        private readonly ISubGroupService _subgroupService;

        private readonly IGalleryService _galleryService;

        public ProductsController(ElectonContext context,
            IProductService productService,
            IGroupServices groupServices,
            ISubGroupService subgroupService,
            IGalleryService galleryService)
        {
            _context = context;
            _productService = productService;
            _groupServices = groupServices;
            _subgroupService = subgroupService;
            _galleryService = galleryService;
        }



        #region Index

        public IActionResult Index(int pageId = 1)
        {
            int take = 6;
            int pagecount = (int)Math.Ceiling((double)_productService.PageCount() / take);
            int skip = (pageId - 1) * take;
            ViewBag.PageCount = pagecount;
            ViewBag.CurrentPage = pageId;

            var list = _productService.GetAll(take, skip);

            return View(list);

        }
        #endregion


        #region Details
        public IActionResult Details(int id)
        {
            var product = _productService.GetForDetails(id);
            if (product == null)
                return NotFound();


            return View(product);
        }

        #endregion

        #region Create

        public IActionResult Create()
        {
            var group = _groupServices.GetBySelectList();
            ViewData["GroupId"] = new SelectList(group, "Id", "Title");
            ViewData["SubGroupId"] = new SelectList(_subgroupService.GetBySelectlist(group.First().Id), "Id", "Title");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateProductViewModel model)
        {

            var group = _groupServices.GetBySelectList();
            #region Validation
            if (!ModelState.IsValid)
            {

                ViewData["GroupId"] = new SelectList(group, "Id", "Title");
                ViewData["SubGroupId"] = new SelectList(_subgroupService.GetBySelectlist(group.First().Id), "Id", "Title");
                return View(model);
            }

            #endregion


            var result = _productService.CreateProduct(model);
            switch (result)
            {
                case Domain.Enums.ResultCreateProduct.Success:

                    TempData["SuccessMessage"] = "افزودن محصول با موفقیت انجام شد";
                    return RedirectToAction("Index");
                case Domain.Enums.ResultCreateProduct.GroupNotValid:
                    TempData["ErrorMessage"] = "ثبت با خطا مواجه شد";
                    break;
                case Domain.Enums.ResultCreateProduct.SubGroupNotValid:
                    TempData["ErrorMessage"] = "ثبت با خطا مواجه شد";
                    break;

            }

            ViewData["GroupId"] = new SelectList(group, "Id", "Title", model.GroupId);
            ViewData["SubGroupId"] = new SelectList(_subgroupService.GetBySelectlist(group.First().Id), "Id", "Title", model.SubGroupId);
            return View(model);
        }
        #endregion

        #region Edit

        public IActionResult Edit(int id)
        {
            var group = _groupServices.GetBySelectList();
            var product = _productService.GetForEditProduct(id);

            if (product == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(group, "Id", "Title", product.GroupId);
            ViewData["SubGroupId"] = new SelectList(_subgroupService.GetBySelectlist(product.GroupId), "Id", "Title", product.SubGroupId);

            ViewBag.Galleries = _galleryService.GetProductgallery(product.Id);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditProductViewModel model)
        {
            var group = _groupServices.GetBySelectList();
            #region Validation
            if (!ModelState.IsValid)
            {
                ViewData["GroupId"] = new SelectList(group, "Id", "Title", model.GroupId);
                ViewData["SubGroupId"] = new SelectList(_subgroupService.GetBySelectlist(model.GroupId), "Id", "Title", model.SubGroupId);

                return View(model);
            }
            #endregion

            var result = _productService.EditProduct(model);

            switch (result)
            {
                case Domain.Enums.ResultEditProduct.Success:
                    TempData["SuccessMessage"] = "ویرایش با موفقیت انجام شد";
                    return RedirectToAction("Index");
                case Domain.Enums.ResultEditProduct.ProductNotFound:
                    TempData["ErrorMessage"] = "مشکلی  در ویرایش محصول رخ داده است ";
                    break;
                case Domain.Enums.ResultEditProduct.GroupNotFound:
                    TempData["ErrorMessage"] = "مشکلی  در ویرایش محصول رخ داده است ";
                    break;
                case Domain.Enums.ResultEditProduct.SubGroupNotFound:
                    TempData["ErrorMessage"] = "مشکلی  در ویرایش محصول رخ داده است ";
                    break;


            }
            ViewData["GroupId"] = new SelectList(group, "Id", "Title", model.GroupId);
            ViewData["SubGroupId"] = new SelectList(_subgroupService.GetBySelectlist(model.GroupId), "Id", "Title", model.SubGroupId);
            return View(model);
        }
        #endregion

        #region Delete

        public IActionResult Delete(int id)
        {
            var result = _productService.DeleteProduct(id);


            return RedirectToAction("Index");
        }


        #endregion

        #region GetSubGroup
        public IActionResult GetSubGroup(int groupId)
        {
            ViewData["SubGroupId"] = new SelectList(_subgroupService.GetBySelectlist(groupId), "Id", "Title");
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
                case Domain.Enums.ResultDeleteGallery.ProductGalleryNotFound:
                    break;

            }
            return Redirect("/Admin/Products/Edit/" + productId + "#gallery");
        }
        #endregion
    }
}
