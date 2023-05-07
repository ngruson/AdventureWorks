using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Infrastructure.Api.Product.Handlers.DuplicateProduct
{
    public class ProductProductPhoto
    {
        public bool Primary { get; set; }

        public ProductPhoto? ProductPhoto { get; set; }
    }
}
