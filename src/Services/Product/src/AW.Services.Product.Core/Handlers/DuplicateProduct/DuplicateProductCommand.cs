using MediatR;

namespace AW.Services.Product.Core.Handlers.DuplicateProduct
{
    public class DuplicateProductCommand : IRequest<Product>
    {
        public string? ProductNumber { get; set; }
    }
}
