using AW.UI.Web.Infrastructure.Api.Product.Handlers.GetProductCategories;
using AW.UI.Web.Store.Mvc.ViewModels.Home;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace AW.UI.Web.Store.Mvc.Controllers
{
    [AuthorizeForScopes(ScopeKeySection = "AuthN:ApiScopes:ProductApiRead")]
    public class HomeController : Controller
    {
        private readonly IMediator mediator;

        public HomeController(IMediator mediator) =>
            this.mediator = mediator;

        public async Task<IActionResult> Index()
        {
            var vm = new HomeViewModel
            {
                ProductCategories = await mediator.Send(new GetProductCategoriesQuery())
            };

            return View(vm);
        }
    }
}
