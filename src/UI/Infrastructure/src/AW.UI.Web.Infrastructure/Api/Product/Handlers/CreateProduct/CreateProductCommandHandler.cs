using AW.UI.Web.Infrastructure.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.Product.Handlers.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly ILogger<CreateProductCommandHandler> _logger;
        private readonly IProductApiClient _client;

        public CreateProductCommandHandler(ILogger<CreateProductCommandHandler> logger, IProductApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Adding product");
            await _client.CreateProduct(request.Product);
            _logger.LogInformation("Product succesfully added");
        }
    }
}
