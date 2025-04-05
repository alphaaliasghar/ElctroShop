using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElctroShop.Domain.Models.User;
using ElctroShop.Infra.Data.Context;
using ElctroShop.Application.Services.Interfaces;
using ElctroShop.Domain.ViewModels;

namespace ElctroShop.Web.Areas.Admin.Controllers
{

    public class UsersController : AdminBaseController
    {
        private readonly ElctroShopContext _context;

        private readonly IUserService _userService;

        public UsersController(ElctroShopContext context,
            IUserService userService)
        {
            _context = context;
            _userService = userService;
        }



        // GET: Admin/Users
        public IActionResult Index(int pageId = 1)
        {
            int take = 5;
            int pagecount = (int)Math.Ceiling((double)_userService.PageCount() / take);

            int skip = (pageId - 1) * take;
            ViewBag.PageCount = pagecount;
            ViewBag.CurrentPage = pageId;

            var list = _userService.GetAll(take, skip);

            return View(list);
        }


        #region Details
        public IActionResult Details(int id)
        {
            var user = _userService.GetUserForDetail(id);
            if (user == null)
                return NotFound();

            return View(user);
        }


        #endregion


        #region Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateUserViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                return View(model);

            }
            #endregion

            var result = _userService.CreateUser(model);
            switch (result)
            {
                case Domain.Enums.ResultCreateUser.Success:
                    TempData["SuccessMessage"] = "ثبت کاربر با موفقیت انجام شد";
                    return RedirectToAction("Index");
                case Domain.Enums.ResultCreateUser.EmailNotValid:
                    TempData["WarningMessage"] = "ایمیل معتبر نمی باشد";
                    break;
                case Domain.Enums.ResultCreateUser.UserNameNotValid:
                    TempData["WarningMessage"] = "نام کاربری معتبر نمی باشد";
                    break;
            }
            return View(model);
        }

        #endregion

        #region Edit
        public IActionResult Edit(int id)
        {
            var user = _userService.EditFotUser(id);
            if (user == null)
                return NotFound();
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateUserViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
                return View(model);
            #endregion

            var result = _userService.EditUser(model);
            switch (result)
            {
                case Domain.Enums.ResultEditUser.Success:
                    TempData["SuccessMessage"] = "ویرایش یا موفقیت انجام شد";
                    return RedirectToAction("Index");
                case Domain.Enums.ResultEditUser.EmailDuplicated:
                    TempData["ErrorMessage"] = "ایمیل تکراری می باشد";
                    break;
                case Domain.Enums.ResultEditUser.UserNameDuplicated:
                    TempData["ErrorMessage"] = "نام کاربری تکراری می باشد";
                    break;

            }

            return View(model);
        }

        #endregion

        #region Delete

        public IActionResult Delete(int id)
        {
          var user=_userService.GetUserForDetail(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

   
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
           var result=_userService.DeleteUser(id);

            return RedirectToAction(nameof(Index));
        }


    }
    #endregion
}
