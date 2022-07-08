using AW.UI.Web.SharedKernel.Product.Handlers.GetProductCategories;
using AW.UI.Web.Store.ViewModels.ProductMenu;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AW.UI.Web.Store.ViewComponents
{
    public class ProductMenu : ViewComponent
    {
        private readonly IMediator mediator;

        public ProductMenu(IMediator mediator) =>
            this.mediator = mediator;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vm = new ProductMenuComponentViewModel
            {
                Categories = await mediator.Send(new GetProductCategoriesQuery())
            };
            return View(vm);
        }
    }
}