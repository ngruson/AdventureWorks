using AW.UI.Web.Admin.Mvc.Extensions;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.Product;
using AW.UI.Web.Admin.Mvc.ViewModels.Product.ModelBinders;
using AW.UI.Web.Infrastructure.Api.Product.Handlers.GetProductCategories;
using AW.UI.Web.Infrastructure.Api.Product.Handlers.GetProductModels;
using AW.UI.Web.Infrastructure.Api.Product.Handlers.GetUnitMeasures;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace AW.UI.Web.Admin.Mvc.Controllers
{
    [AuthorizeForScopes(ScopeKeySection = "AuthN:ApiScopes:ProductApiRead")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMediator _mediator;
        private readonly IProductService _productService;

        public static readonly string CATEGORIES = "categories";
        public static readonly string CLASSES = "classes";
        public static readonly string PRODUCTLINES = "productLines";
        public static readonly string PRODUCTMODELS = "productModels";
        public static readonly string STYLES = "styles";
        public static readonly string SUBCATEGORIES = "subcategories";
        public static readonly string UNITMEASURES = "unitMeasures";

        public ProductController(
            ILogger<ProductController> logger,
            IMediator mediator, 
            IProductService productService
        )
        {
            _logger = logger;
            _mediator = mediator;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            return View(
                await _productService.GetProducts()
            );
        }

        public async Task<IActionResult> Detail(string productNumber)
        {
            var product = await _productService.GetProductDetail(
                productNumber
            );

            await GetViewData();

            var categories = (await _mediator.Send(new GetProductCategoriesQuery()))
                .OrderBy(_ => _.Name)
                .ToList();

            ViewData[CATEGORIES] = categories
                .ToSelectList(_ => _.Name, _ => _.Name);

            if (!string.IsNullOrEmpty(product.Product!.ProductCategoryName))
                ViewData[SUBCATEGORIES] = categories
                    .Single(c => c.Name == product.Product!.ProductCategoryName)
                    .Subcategories!
                    .ToSelectList(_ => _.Name, _ => _.Name);
            else
                ViewData[SUBCATEGORIES] = new List<ProductSubcategory>()
                    .ToSelectList(_ => _.Name, _ => _.Name);

            return View(product);
        }

        public async Task<IActionResult> AddProduct()
        {
            await GetViewData();

            var categories = (await _mediator.Send(new GetProductCategoriesQuery()))
                .OrderBy(_ => _.Name)
                .ToList();

            ViewData[CATEGORIES] = categories
                .ToSelectList(_ => _.Name, _ => _.Name);

            ViewData[SUBCATEGORIES] = new List<ProductSubcategory>()
                .ToSelectList(_ => _.Name, _ => _.Name);

            var viewModel = new ProductDetailViewModel
            {
                Product = new ProductViewModel
                {
                    SellStartDate = DateTime.Today
                }
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([ModelBinder(BinderType = typeof(AddProductViewModelBinder))] AddProductViewModel viewModel)
        {
            await _productService.AddProduct(viewModel);

            return RedirectToAction(
                nameof(Detail),
                new { productNumber = viewModel.Product!.ProductNumber }
            );
        }

        public async Task<IActionResult> UpdateProduct([ModelBinder(BinderType = typeof(EditProductViewModelBinder))] EditProductViewModel viewModel)
        {
            await _productService.UpdateProduct(viewModel);

            return RedirectToAction(
                nameof(Detail),
                new { productNumber = viewModel.Product!.ProductNumber }
            );
        }

        public async Task<IActionResult> UpdatePricing([ModelBinder(BinderType = typeof(EditPricingViewModelBinder))] EditPricingViewModel viewModel)
        {
            _logger.LogInformation(
                "Update pricing for product number {ProductNumber}: {@ViewModel}",
                viewModel.Product!.ProductNumber, 
                viewModel
            );

            await _productService.UpdatePricing(viewModel);

            return RedirectToAction(
                nameof(Detail),
                new { productNumber = viewModel.Product.ProductNumber }
            );
        }

        public async Task<IActionResult> UpdateProductOrganization(EditProductOrganizationViewModel viewModel)
        {
            _logger.LogInformation("Updating product organization");

            await _productService.UpdateProductOrganization(viewModel);

            _logger.LogInformation("Product organization succeeded. Redirecting to detail page");

            return RedirectToAction(
                nameof(Detail),
                new { productNumber = viewModel.Product!.ProductNumber }
            );
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProducts([FromBody] string[] productNumbers)
        {
            foreach (var productNumber in productNumbers)
            {
                await _productService.DeleteProduct(productNumber);
            }

            return new OkResult();
        }

        public async Task<IActionResult> DeleteProduct(string productNumber)
        {
            await _productService.DeleteProduct(productNumber);

            return RedirectToAction(
                nameof(Index)
            );
        }

        public async Task<IActionResult> DuplicateProduct(string productNumber)
        {
            var product = await _productService.DuplicateProduct(productNumber);

            return RedirectToAction(
                nameof(Detail),
                new { product.ProductNumber }
            );
        }

        private async Task GetViewData()
        {
            ViewData[CLASSES] = new List<string>
            {
                "High",
                "Medium",
                "Low"
            }
            .ToSelectList(_ => _, _ => _);


            ViewData[PRODUCTLINES] = new List<string>
            {
                "Mountain",
                "Road",
                "Standard",
                "Touring"
            }
            .ToSelectList(_ => _, _ => _);

            ViewData[PRODUCTMODELS] = (await _mediator.Send(new GetProductModelsQuery()))
                .OrderBy(_ => _.Name)
                .ToList()
                .ToSelectList(_ => _.Name, _ => _.Name);

            ViewData[STYLES] = new List<string>
            {
                "Mens",
                "Womens",
                "Universal"
            }
            .ToSelectList(_ => _, _ => _);

            ViewData[UNITMEASURES] = (await _mediator.Send(new GetUnitMeasuresQuery()))
                .OrderBy(_ => _.Name)
                .ToList()
                .ToSelectList(_ => _.UnitMeasureCode!.Trim(), _ => $"{_.UnitMeasureCode!.Trim()} ({_.Name})");
        }

        public async Task<JsonResult> GetSubcategories(string categoryName)
        {
            var category = await _productService.GetCategory(categoryName);
            return Json(category.Subcategories);
        }
    }
}
