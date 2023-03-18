using AW.SharedKernel.AutoMapper;

namespace AW.Services.Product.Core.Handlers.GetProductModels
{
    public class ProductModel : IMapFrom<Entities.ProductModel>
    {
        public string? Name { get; set; }

        public string? CatalogDescription { get; set; }

        public string? Instructions { get; set; }
    }
}
