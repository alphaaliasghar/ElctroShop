using ElectonShop.Application.Services.Interface;
using ElectonShop.Domain.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace ElectonShop.Web.Controllers
{

    public class AccountController : BaseController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        #region Register

        [HttpGet("/register-user")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost("/register-user")]
        public IActionResult Register(RegisterViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
                return View(model);
            #endregion

            if (!model.Roulse)
            {
                ModelState.AddModelError("", "قوانین را نپذیرفته اید .");
                return View(model);
            }

            var result = _userService.RegisterUser(model);
            switch (result)
            {

                case Domain.Enums.ResultRegisterUser.EmailNotValid:
                    TempData["WarrningMessage"] = "ایمیل معتبر نمی باشد";
                    break;
                case Domain.Enums.ResultRegisterUser.UserNameNotValid:
                    TempData["WarrningMessage"] = "نام کاربری معتبر نمی باشد";
                    break;
                case Domain.Enums.ResultRegisterUser.Success:
                    TempData["SuccessMessage"] = "ثبت نام با موفقیت انجام شد";
                    return View("SuccessRegister", model);
            }
            return View();
        }
        #endregion

        #region Login
        [HttpGet("/login-user")]
        public IActionResult Login(string ReturnURL = "/")
        {
            ViewBag.ReturnURL = ReturnURL;
            return View();
        }
        [HttpPost("/login-user")]
        public IActionResult Login(LoginViewModel model, string? ReturnURL)
        {
            #region Validation
            if (!ModelState.IsValid)
                return View(model);
            #endregion

            var user = _userService.LoginUser(model);
            if (user == null)
            {
                ModelState.AddModelError("UserName", " کاربری یافت نشد");
                return View(model);
            }

            if (user.IsDelete)
            {
                ModelState.AddModelError("", "حساب کاربری شما مسدود شده است لطفا با پشتیبانی تماس بگیرید");
                return View(model);
            }
            
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email),
                 new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim("IsAdmin",user.IsAdmin.ToString())
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                IsPersistent=model.RememberMy
            };
            HttpContext.SignInAsync(principal, properties);

          
            return Redirect(ReturnURL ?? "/");
        }
        #endregion

        #region Logout
        [Route("/logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/login-user");
        }
        #endregion
    }
}
