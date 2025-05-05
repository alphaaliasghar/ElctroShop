using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElectonShop.Domain.Models.User;
using ElectonShop.Infra.Data.Context;
using ElectonShop.Application.Services.Interface;
using ElectonShop.Domain.ViewModels;

namespace ElectonShop.Web.Areas.Admin.Controllers
{

    public class UsersController : AdminBaseController
    {
        private readonly ElectonContext _context;

        private readonly IUserService _userService;

        public UsersController(ElectonContext context,
            IUserService userService)
        {
            _context = context;
            _userService = userService;
        }



        #region List
        // GET: Admin/Users
        public IActionResult Index(int pageId = 1)
        {
            int take = 6;
            int pagecount = (int)Math.Ceiling((double)_userService.CountPage() / take);
            int skip = (pageId - 1) * take;
            ViewBag.PageCount = pagecount;
            ViewBag.CurrentPage = pageId;
            var list = _userService.GetAll(take, skip);
            return View(list);
        }
        #endregion


        #region Detail
        public IActionResult Details(int id)
        {
            var user = _userService.GetForDetail(id);
            if (user == null)
            {
                return NotFound();
            }
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
            var result = _userService.CreateUser(model);

            switch (result)
            {
                case Domain.Enums.ResultCreateUser.Success:
                    TempData["SuccessMessage"] = "کاربر با موفقیت اضافه شد";
                    return RedirectToAction("Index");
                case Domain.Enums.ResultCreateUser.EmailNotValid:

                    TempData["ErrorMessage"] = "ایمیل معتبر نمی باشد";

                    break;
                case Domain.Enums.ResultCreateUser.UserNameNotValid:
                    TempData["ErrorMessage"] = "نام کاربری معتبر نمی باشد";
                    break;
            }


            return View(result);
        }
        #endregion

        #region Edit

        public IActionResult Edit(int id)
        {
            var user = _userService.GetForEditUser(id);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditUserViewModel model)
        {

            var result = _userService.EditUser(model);

            switch (result)
            {
                case Domain.Enums.ResultEditUser.Success:
                    TempData["SuccessMessage"] = "ویرایش با موفقیت انجام شد";
                    return RedirectToAction("Index");
                case Domain.Enums.ResultEditUser.EmailDuplicated:

                    TempData["WarningMessage"] = " ایمیل تکراری است";
                    break;
                case Domain.Enums.ResultEditUser.UserNameDuplicated:
                    TempData["WarningMessage"] = "نام کاربری تکراری است";
                    break;

                case Domain.Enums.ResultEditUser.UserNotFound:
                    TempData["ErrorMessage"] = "کاربری پیدا نشد";
                    break;

            }

            return View();
        }
        #endregion

       public IActionResult DeleteUser(int id)
        {
            var result = _userService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

     
    }
}
