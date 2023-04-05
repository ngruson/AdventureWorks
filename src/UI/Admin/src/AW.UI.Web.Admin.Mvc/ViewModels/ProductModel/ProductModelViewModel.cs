using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.ProductModel
{
    public class ProductModelViewModel : IMapFrom<SharedKernel.Product.Handlers.GetProductModels.ProductModel>
    {
        public string? Name { get; set; }

        public string? CatalogDescription { get; set; }

        public string? Instructions { get; set; }
    }
}
