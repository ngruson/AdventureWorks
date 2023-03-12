using AW.UI.Web.Admin.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace AW.UI.Web.Admin.Mvc.Controllers
{
    [AuthorizeForScopes(ScopeKeySection = "AuthN:ApiScopes:ProductApiRead")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService
        )
        {
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
            var product = await _productService.GetProduct(
                productNumber
            );

            return View(product);
        }
    }
}
