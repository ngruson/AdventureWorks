using Ardalis.GuardClauses;
using AutoMapper;
using AW.SharedKernel.Extensions;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProductCategories;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProducts;
using AW.UI.Web.Store.ViewModels;
using AW.UI.Web.Store.ViewModels.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AW.UI.Web.Store.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductController(ILogger<ProductController> logger, IMapper mapper, IMediator mediator) =>
            (_logger, _mapper, _mediator) = (logger, mapper, mediator);

        private static List<SelectListItem> GetPageSizeList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem("12", "12"),
                new SelectListItem("25", "25"),
                new SelectListItem("50", "50"),
                new SelectListItem("75", "75"),
                new SelectListItem("100", "100")
            };
        }

        public async Task<IActionResult> Index(int? page, int? pageSize, string productCategory, string? productSubcategory)
        {
            Guard.Against.NullOrEmpty(productCategory, _logger);
            ViewData["pageSizeList"] = GetPageSizeList();

            var products = await _mediator.Send(new GetProductsQuery(
                    page ?? 0, pageSize ?? 12, productCategory, productSubcategory
                )
            );

            var totalPages = int.Parse(Math.Ceiling(((decimal)products.TotalProducts / (pageSize ?? 12))).ToString());

            var vm = new ProductsViewModel
            {
                Title = productSubcategory ?? productCategory,
                ProductCategory = productCategory,
                ProductSubcategory = productSubcategory!,
                ProductCategories = await _mediator.Send(new GetProductCategoriesQuery()),
                Products = _mapper.Map<List<ProductViewModel>>(products.Products),
                PaginationInfo = new PaginationInfoViewModel(
                    products.TotalProducts,
                    products.Products.Count,
                    page ?? 0,
                    totalPages,
                    ((page ?? 0) == 0) ? "disabled" : "",
                    ((page ?? 0) == totalPages - 1) ? "disabled" : ""
                )
            };

            return View(vm);
        }
    }
}