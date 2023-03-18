using AW.UI.Web.Admin.Mvc.Extensions;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.Product;
using AW.UI.Web.Admin.Mvc.ViewModels.Product.ModelBinders;
using AW.UI.Web.Admin.Mvc.ViewModels.SalesOrder.ModelBinders;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProductModels;
using AW.UI.Web.SharedKernel.Product.Handlers.GetUnitMeasures;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Web;

namespace AW.UI.Web.Admin.Mvc.Controllers
{
    [AuthorizeForScopes(ScopeKeySection = "AuthN:ApiScopes:ProductApiRead")]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IProductService _productService;

        public ProductController(IMediator mediator, IProductService productService
        )
        {
            _mediator = mediator;
            _productService = productService;
        }

        public async Task<IActionResult> Index(int? pageId)
        {
            return View(
                await _productService.GetProducts(
                    pageId ?? 0,
                    Constants.ITEMS_PER_PAGE
                )
            );
        }

        public async Task<IActionResult> Detail(string productNumber)
        {
            var product = await _productService.GetProductDetail(
                productNumber
            );

            ViewData["classes"] = new List<SelectListItem>
            {
                new SelectListItem("High", "H"),
                new SelectListItem("Medium", "M"),
                new SelectListItem("Low", "L")
            };

            ViewData["productLines"] = new List<SelectListItem>
            {
                new SelectListItem("Mountain", "M"),
                new SelectListItem("Road", "R"),
                new SelectListItem("Standard", "S"),
                new SelectListItem("Touring", "T")
            };

            ViewData["productModels"] = (await _mediator.Send(new GetProductModelsQuery()))
                .OrderBy(_ => _.Name)
                .ToList()
                .ToSelectList(_ => _.Name, _ => _.Name);

            ViewData["styles"] = new List<SelectListItem>
            {
                new SelectListItem("Mens", "M"),
                new SelectListItem("Womens", "W"),
                new SelectListItem("Universal", "U")
            };

            ViewData["unitMeasures"] = (await _mediator.Send(new GetUnitMeasuresQuery()))
                .OrderBy(_ => _.Name)
                .ToList()
                .ToSelectList(_ => _.UnitMeasureCode!.Trim(), _ => $"{_.UnitMeasureCode!.Trim()} ({_.Name})");

            return View(product);
        }

        public async Task<IActionResult> UpdateProduct([ModelBinder(BinderType = typeof(UpdateProductViewModelBinder))] UpdateProductViewModel viewModel)
        {
            await _productService.UpdateProduct(viewModel);

            return RedirectToAction(
                nameof(Detail),
                new { productNumber = viewModel.Product!.ProductNumber }
            );
        }
    }
}
