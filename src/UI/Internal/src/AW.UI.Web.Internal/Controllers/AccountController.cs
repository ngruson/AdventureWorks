using Microsoft.AspNetCore.Mvc;

namespace AW.UI.Web.Internal.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}