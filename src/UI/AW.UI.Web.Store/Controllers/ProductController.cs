using AW.UI.Web.Store.Services;
using AW.UI.Web.Store.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.UI.Web.Store.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService) =>
            (this.productService) = (productService);

        private List<SelectListItem> GetPageSizeList()
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

        public async Task<IActionResult> Index(int? page, int? pageSize, string productCategory, string productSubcategory)
        {
            ViewData["pageSizeList"] = GetPageSizeList();
            return View(
                await productService.GetProducts(
                    page ?? 0, 
                    pageSize ?? 12,
                    productCategory,
                    productSubcategory
                )
            );
        }

        [HttpPost]
        public async Task<IActionResult> Index(ProductsViewModel viewModel)
        {
            return View(
                await productService.GetProducts(
                    0,
                    viewModel.PageSize,
                    viewModel.ProductCategory,
                    viewModel.ProductSubcategory
                )
            );
        }
    }
}