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
using Microsoft.AspNetCore.Components.Server.Circuits;
using ElctroShop.Domain.Enums;

namespace ElctroShop.Web.Areas.Admin.Controllers
{

    public class ProductGroupsController : AdminBaseController
    {


        private readonly IProductGroupService _productGroupService;

        private readonly ISubGroupService _subGroupService;

        public ProductGroupsController(IProductGroupService productGroupService,
            ISubGroupService subGroupService)
        {
            _productGroupService = productGroupService;
            _subGroupService = subGroupService;
        }


        #region List
        public IActionResult Index(int pageId = 1)
        {
            int take = 6;
            int pagecount = (int)Math.Ceiling((double)_productGroupService.Count() / take);
            int skip = (pageId - 1) * take;

            ViewBag.PageCount = pagecount;
            ViewBag.CurrentPage = pageId;

            var group = _productGroupService.GetAll(take, skip);

            return View(group);
        }
        #endregion



        #region Details

        public IActionResult Details(int id)
        {
            var group = _productGroupService.GetDetailById(id);
            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }
        #endregion



        #region Create

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateProductGroupViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
                return View(model);
            #endregion

            var result = _productGroupService.Create(model);
            switch (result)
            {
                case Domain.Enums.ResultCreateProductGroup.Success:

                    TempData["SuccessMessage"] = " افزودن با موفقیت انجام شد";
                    return RedirectToAction("Index");
            }

            return View(model);
        }
        #endregion


        #region Edit
        public IActionResult Edit(int id)
        {
            var group = _productGroupService.GetForEdit(id);

            if (group == null)
                return NotFound();
            return View(group);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UpdateProductGroupViewModel model)
        {

            #region Validation
            if (!ModelState.IsValid)
                return View(model);
            #endregion

            var result = _productGroupService.EditProductGroup(model);
            switch (result)
            {
                case Domain.Enums.ResultEditProductGroup.Success:
                    TempData["SuccessMessage"] = "ویرایش با موفقیت انجام شد";
                    return RedirectToAction("Index");
                case Domain.Enums.ResultEditProductGroup.ProductGroupNotFound:

                    TempData["ErrorMessage"] = "سر گروه پیدا نشد";
                    return View(model);
                case Domain.Enums.ResultEditProductGroup.SubGroupNotFound:

                    TempData["ErrorMessage"] = "زیر گروه پیدا نشد";
                    return View(model);
            }
            return View(model);
        }

        #endregion




        #region Delete

        public IActionResult Delete(int id)
        {
            var group = _productGroupService.GetDetailById(id);

            if (group == null)
                return NotFound();

            return View(group);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _productGroupService.DeleteProductGroup(id);
            return RedirectToAction(nameof(Index));
        }

        #endregion



        #region CreateSubGroup
        public IActionResult CreateSubgroup(int groupId)
        {

            return View();
        }
        [HttpPost]
        public IActionResult CreateSubgroup(CreateSubgroupViewModel model)
        {

            #region Validation
            if (!ModelState.IsValid)
                return View(model);
            #endregion

            var result = _subGroupService.CreateSubGroup(model);

            switch (result)
            {
                case Domain.Enums.ResultCreateSubGroup.Success:

                    TempData["SuccessMessage"] = "عملیات با موفقیت انجام شد";
                    return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        #endregion

        #region EditSubGroup

        [HttpGet("/admin/editsubgroup/{groupId}/{subGroupId}")]
        public ActionResult EditSubgroup(int groupId, int subGroupId)
        {
            var sub = _subGroupService.GetForEdit(groupId,subGroupId);

            if (sub == null)

                return NotFound();
            return View(sub);
        }
        [HttpPost("/admin/editsubgroup/{groupId}/{subGroupId}")]
        public ActionResult EditSubgroup(UpdateSubgroupViewModel model)
        {
            var result = _subGroupService.UpdateSubGroup(model);

            switch (result)
            {
                case ResultEditSubGroup.Success:

                    TempData["SuccessMessage"] = "ویرایش با موفقیت انجام شد";
                    return RedirectToAction(nameof(Index));
                case ResultEditSubGroup.SubGroupNotFound:
                    TempData["ErrorMessage"] = "ویرایش با خطا شد";
                    break;
            }
            return View(model);
        }
        #endregion

        #region DeleteSubGroup
     
        public IActionResult DeleteSubGroup(int subgroupId)
        {
            var deletesubgroup = _subGroupService.DeleteSubGroup(subgroupId);

            return RedirectToAction(nameof(Index));
        }

     
        #endregion
    }
}
