using Microsoft.AspNetCore.Mvc;

namespace ElctroShop.Web.Controllers
{
    public class BaseController : Controller 
    {
        protected string SuccessMessage = "SuccessMessage";
        protected string ErrorMessage = "ErrorMessage";
        protected string InfoMessage = "InfoMessage";
        protected string WarningMessage = "WarningMessage";
    }
}
