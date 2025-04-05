using ElctroShop.Application.Services.Interfaces;
using ElctroShop.Domain.Enums;
using ElctroShop.Domain.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ElctroShop.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        #region Register

        [HttpGet("Register-user")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost("Register-user")]
        public IActionResult Register(RegisterViewModel model)
        {

            #region Validation
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #endregion

            if (!model.Rouls)
            {
                TempData["ErrorMessage"] = "لطفا قوانین سایت را بپزیرید!";
                return View(model);
            }

            var result = _userService.RegisterUser(model);
            switch (result)
            {

                case ResultRegisterUser.EmailNotValid:
                    {
                        TempData["ErrorMessage"] = "ایمیل  معتبر نمی باشد";
                        break;
                    }
                case ResultRegisterUser.UserNameNotValid:
                    {
                        TempData["ErrorMessage"] = "نام کاربری  معتبر نمی باشد";
                        break;
                    }
                case ResultRegisterUser.Success:
                    {
                        TempData["SuccessMessage"] = "ثبت نام با موفقیت انجلم شد";

                        return View("SuccessRegister", model);
                    }
            }
            return View();
        }
        #endregion

        #region Login
        [HttpGet("Login-User")]
        public IActionResult Login(string ReturnURL = "/")
        {
            ViewBag.ReturnURL = ReturnURL;
            return View();
        }
        [HttpPost("Login-User")]
        public IActionResult Login(LoginViewModel model, string? ReturnURL)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #endregion

            var result = _userService.LoginUser(model);

            if (result == null)
            {
                TempData["ErrorMessage"]
                  = " کاربری یافت نشد";

                return View(model);
            }

            if (result.IsDelete)
            {
                TempData["ErrorMessage"]
                    = "حساب کاربری شما مسدود شده است لطفا با پشتیبانی تماس بگیرید";

                return View(model);
            }

            List<Claim> Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,result.Email),
                new Claim(ClaimTypes.NameIdentifier,result.Id.ToString()),
                new Claim(ClaimTypes.Name,result.UserName),
                new Claim("IsAdmin",result.IsAdmin.ToString())
            };
            ClaimsIdentity identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                IsPersistent = model.RememberMy,
               

            };
            HttpContext.SignInAsync(principal, properties);

            return Redirect(ReturnURL ?? "/");
        }


        #endregion

        #region Logout
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login-User");
        }
        #endregion
    }
}
