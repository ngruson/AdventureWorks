using AW.SharedKernel.AutoMapper;

namespace AW.Services.Product.Core.Handlers.GetProduct
{
    public class ProductProductPhoto : IMapFrom<Entities.ProductProductPhoto>
    {
        public bool Primary { get; set; }

        public ProductPhoto? ProductPhoto { get; set; }
    }
}
