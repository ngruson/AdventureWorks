using MediatR;

namespace AW.Services.Product.Core.Handlers.DeleteProduct
{
    public class DeleteProductCommand : IRequest
    {
        public string? ProductNumber { get; set; }
    }
}
