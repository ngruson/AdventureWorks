﻿using Ardalis.GuardClauses;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.SharedKernel.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Product.Handlers.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly ILogger<UpdateProductCommandHandler> _logger;
        private readonly IProductApiClient _client;

        public UpdateProductCommandHandler(ILogger<UpdateProductCommandHandler> logger, IProductApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request.Product, _logger);

            _logger.LogInformation("Updating product");
            await _client.UpdateProduct(request.Product!);
            _logger.LogInformation("Product succesfully updated");

            return Unit.Value;
        }
    }
}
