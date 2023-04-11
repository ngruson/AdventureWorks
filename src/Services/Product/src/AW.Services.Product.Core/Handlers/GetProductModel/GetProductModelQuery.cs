using MediatR;

namespace AW.Services.Product.Core.Handlers.GetProductModel
{
    public class GetProductModelQuery : IRequest<ProductModel?>
    {
        public string? Name { get; set; }
    }
}
