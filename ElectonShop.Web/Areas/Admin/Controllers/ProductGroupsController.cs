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
using ElectonShop.Domain.ViewModels.Product;

namespace ElectonShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductGroupsController : AdminBaseController
    {
        private readonly ISubGroupService _subgroupService;

        private readonly IGroupServices _groupService;

        public ProductGroupsController(ISubGroupService subgroupService,
            IGroupServices groupService)
        {
            _subgroupService = subgroupService;
            _groupService = groupService;
        }




        #region List

        public IActionResult Index(int pageId = 1)
        {
            int take = 6;
            int pagecount = (int)Math.Ceiling((double)_groupService.PageCount() / take);
            int skip = (pageId - 1) * take;
            ViewBag.PageCount = pagecount;
            ViewBag.CurrentPage = pageId;

            var list = _groupService.GetAll(take, skip);
            return View(list);
        }
        #endregion


        #region Details
        public IActionResult Details(int id)
        {
            var productgroup = _groupService.GetForDetailGroup(id);

            if (productgroup == null)
                return NotFound();

            return View(productgroup);
        }

        #endregion

        #region Create
        public IActionResult Create()
        {
            return PartialView();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductGroupViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #endregion

            var result = _groupService.CreateProductGroup(model);
            switch (result)
            {
                case Domain.Enums.ResultCreateProductGroup.Success:
                    TempData["SuccessMessage"] = "افزودن با موفقیت انجام شد";
                    return RedirectToAction("Index");

            }
            return View(model);
        }
        #endregion

        #region Edit

        public IActionResult Edit(int id)
        {
            var group = _groupService.GetForEdit(id);
            if (group == null)
            {
                return NotFound();
            }

            return PartialView(group);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UpdateProductGroupViewModel model)
        {
            var result = _groupService.EditProductGroup(model);
            switch (result)
            {
                case Domain.Enums.ResultEditProductGroup.Success:
                    TempData["SuccessMessage"] = "ویرایش سر گروه با موفقیت انجام شد";
                    return RedirectToAction("Index");

                case Domain.Enums.ResultEditProductGroup.ProductGroupNotValid:
                    TempData["ErrorMessage"] = "گروه مورد نظر پیدا نشد";
                    break;

            }

            return View(model);
        }

        #endregion

        #region Delete
        public IActionResult Delete(int id)
        {

            var result = _groupService.DeleteProductgroup(id);

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region CreateSubgroup
        public IActionResult CreateSubGroup( int groupId)
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateSubGroup(CreateSubgroupViewModel model)
        {
            var result=_subgroupService.CreateSubGroup(model);

            switch (result)
            {
                case Domain.Enums.ResultCreateSubGroup.Success:
                    TempData["SuccessMessage"] = "افزودن زیر گروه  با موفقیت انجام شد";
                    return RedirectToAction("Index");

            }
            return View(model);
        }
        #endregion

        #region EditSubGroup
        [HttpGet]
        public IActionResult EditSubgroup(int groupId,int subgroupId)
        {
            var sub=_subgroupService.GetForEdit(groupId,subgroupId);
            if (sub == null)
            {
                return NotFound(); 
            }
            return View(sub);
        }
        [HttpPost]
        public IActionResult EditSubgroup(UpdateSubgroupViewModel model)
        {
            var result =_subgroupService.UpdateSubGroup(model);
            switch (result)
            {
                case Domain.Enums.ResultEditSubGroup.Success:
                    TempData["SuccessMessage"] = "ویرایش زیر گروه با موفقیت انجام شد";
                    return RedirectToAction("Index");
                  
                case Domain.Enums.ResultEditSubGroup.SubGroupNotFound:
                    TempData["ErrorMessage"] = "مشکلی پیش آمد لطفا دوباره امتحان کنید";
                    break;
              
            }
            return View(model);
        }
        #endregion

        #region DeleteSubgroup
        public IActionResult DeleteSubgroup(int groupId,int subgroupId)
        {
            var result=_subgroupService.DeleteSubGroup(groupId,subgroupId);
            return RedirectToAction("Index");
        }
        #endregion

    }
}
