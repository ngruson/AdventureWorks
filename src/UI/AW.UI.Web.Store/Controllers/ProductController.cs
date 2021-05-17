using AutoMapper;
using AW.UI.Web.Store.Services;
using AW.UI.Web.Store.ViewModels;
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
        private readonly IMapper mapper;
        private readonly IProductService productService;

        public ProductController(IMapper mapper, IProductService productService) =>
            (this.mapper, this.productService) = (mapper, productService);

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

            var products = await productService.GetProductsAsync(page ?? 0, pageSize ?? 12, productCategory, productSubcategory);

            var vm = new ProductsViewModel
            {
                Title = productSubcategory ?? productCategory,
                ProductCategory = productCategory,
                ProductSubcategory = productSubcategory,
                ProductCategories = await productService.GetCategoriesAsync(),
                Products = mapper.Map<List<ProductViewModel>>(products.Products),
                PaginationInfo = new PaginationInfoViewModel()
                {
                    ActualPage = page ?? 0,
                    ItemsPerPage = products.Products.Count,
                    TotalItems = products.TotalProducts,
                    TotalPages = int.Parse(Math.Ceiling(((decimal)products.TotalProducts / (pageSize ?? 12))).ToString())
                }
            };

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "disabled" : "";

            return View(vm);
        }
    }
}