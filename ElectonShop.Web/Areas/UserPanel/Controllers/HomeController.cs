using ElctroShop.Web.Areas.UserPanel.Controllers;
using ElectonShop.Application.Services.Interface;
using ElectonShop.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ElectonShop.Web.Areas.UserPanel.Controllers
{
  
    public class HomeController : UserPanelBaseController
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        #region Index

        public IActionResult Index()
        {

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (userId == null)
            {
                return RedirectToAction("/Login", "Account");
            }
            var information = _userService.GetInformationForShow(userId);
            if (information == null)
            {
                return NotFound();
            }
            return View(information);
        }
        #endregion     


        #region ChangePassword

        [HttpGet("ChangePassword-user")]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost("ChangePassword-user")]
        public IActionResult ChangePassword(ChangePaswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var CurrentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = _userService.ChangePassword(CurrentUserId, model);

            ViewBag.Result = result;
            return View();



        }
        #endregion

        #region EditInformation
        [HttpGet("EditInformation-user")]
        public IActionResult EditInformation(int id)
        {
            var user = _userService.GetForEdit(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost("EditInformation-user")]
        public IActionResult EditInformation(EditInformationViewModel model)
        {
            var result=_userService.EditInformation(model);
            switch (result)
            {
                case Domain.Enums.ResultEditInformation.Success:
                    TempData["SuccessMessage"] = "ویرایش حساب کاربری با موفقیت انجام شد";
                    return RedirectToAction("Index");

                case Domain.Enums.ResultEditInformation.UserInformationNotFound:
                    TempData["ErrorMessage"] = "مشکلی در ویرایش پیش آمده لطفا بعدا تلاش کنید";
                    break;
            }
            return View();
        }
        #endregion
    }
}
