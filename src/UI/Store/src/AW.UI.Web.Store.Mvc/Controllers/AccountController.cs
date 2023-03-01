using AW.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AW.UI.Web.Store.Mvc.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IApplication application;
        private readonly ILogger<AccountController> logger;

        public AccountController(IApplication application, ILogger<AccountController> logger) => 
            (this.application, this.logger) = (application, logger);

        [Authorize]
        public async Task<IActionResult> SignIn(string returnUrl)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            logger.LogInformation("----- User {@User} authenticated into {AppName}", User, application.AppName);

            if (token != null)
            {
                ViewData["access_token"] = token;
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public async Task<IActionResult> Signout()
        {
            await HttpContext.SignOutAsync("cookie");
            await HttpContext.SignOutAsync("oidc");

            var homeUrl = Url.Action(nameof(HomeController.Index), "Home");
            return new SignOutResult("cookie",
                new AuthenticationProperties { RedirectUri = homeUrl }
            );
        }
    }
}
