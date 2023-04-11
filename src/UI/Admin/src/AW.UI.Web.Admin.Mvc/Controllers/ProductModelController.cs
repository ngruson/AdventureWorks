using AW.UI.Web.Admin.Mvc.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace AW.UI.Web.Admin.Mvc.Controllers
{
    [AuthorizeForScopes(ScopeKeySection = "AuthN:ApiScopes:ProductApiRead")]
    public class ProductModelController : Controller
    {
        private readonly ILogger<ProductModelController> _logger;
        private readonly IMediator _mediator;
        private readonly IProductModelService _productModelService;

        public ProductModelController(
            ILogger<ProductModelController> logger,
            IMediator mediator,
            IProductModelService productModelService
        )
        {
            _logger = logger;
            _mediator = mediator;
            _productModelService = productModelService;
        }

        public async Task<IActionResult> Index()
        {
            return View(
                await _productModelService.GetProductModels()
            );
        }

        public async Task<IActionResult> Detail(string name)
        {
            return View(
                await _productModelService.GetProductModel(name)
            );
        }
    }
}
