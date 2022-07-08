using AW.UI.Web.SharedKernel.Product.Handlers.GetProductCategories;
using AW.UI.Web.Store.ViewModels.Home;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AW.UI.Web.Store.Controllers
{
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