using AW.SharedKernel.AutoMapper;

namespace AW.Services.Product.Core.Handlers.DuplicateProduct
{
    public class ProductProductPhoto : IMapFrom<CreateProduct.ProductProductPhoto>
    {
        public bool Primary { get; set; }

        public ProductPhoto? ProductPhoto { get; set; }
    }
}
