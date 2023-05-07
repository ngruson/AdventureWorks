using AW.UI.Web.Infrastructure.Api.Product.Handlers.GetProductCategories;
using AW.UI.Web.Store.Mvc.ViewModels.ProductMenu;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AW.UI.Web.Store.Mvc.ViewComponents
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
