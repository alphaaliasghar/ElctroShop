using ElctroShop.Application.Services.Interfaces;
using ElctroShop.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ElctroShop.Web.Areas.UserPanel.Controllers
{

    public class HomeController : UserPanelBaseController
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult UserPanel()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var result = _userService.GetUserForShow(userId);
           if (result == null)
                return NotFound();
            return View(result);
        }

        #region ChangePassword

        [HttpGet("ChangePassword-User")]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost("ChangePassword-User")]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);


            int CurrentUserId = (int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            var result = _userService.ChanePassword(CurrentUserId, model);

            ViewBag.Result = result;
            return View();
        }
        #endregion

        #region EdiuAccount
        public IActionResult EditAccount(int id)
        {
            var account=_userService.GetForEditAccount(id);

            if (account == null)
                return NotFound();
            return View(account);
        }
        [HttpPost]
        public IActionResult EditAccount(EditAccountViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
                return View(model);
            #endregion

            var result =_userService.EditAccount(model);
            switch (result)
            {
                case Domain.Enums.ResultEditAccount.Success:

                    TempData["SuccessMessage"] = "ویرایش حساب کاربری با موفقیت انجام شد";

                    return RedirectToAction("UserPanel");
                case Domain.Enums.ResultEditAccount.UserAccountNotFound:

                    TempData["ErrorMessage"] = "مشکلی در ویرایش پیش آمده است لطفا بعدا تلاش کنید";
                    break;
                
            }
            return View();
        }
        #endregion
    }
}
