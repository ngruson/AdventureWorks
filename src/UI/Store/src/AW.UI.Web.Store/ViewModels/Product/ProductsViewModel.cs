using System.Collections.Generic;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProductCategories;

namespace AW.UI.Web.Store.ViewModels.Product
{
    public class ProductsViewModel
    {
        public string Title { get; set; }
        public string ProductCategory { get; set; }
        public string ProductSubcategory { get; set; }
        public int PageSize { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<ProductViewModel> Products { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}