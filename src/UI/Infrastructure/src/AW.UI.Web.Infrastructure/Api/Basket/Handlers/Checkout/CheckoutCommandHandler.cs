using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.Basket.Handlers.Checkout;

public class CheckoutCommandHandler : IRequestHandler<CheckoutCommand>
{
    private readonly ILogger<CheckoutCommandHandler> _logger;
    private readonly IBasketApiClient _client;

    public CheckoutCommandHandler(ILogger<CheckoutCommandHandler> logger, IBasketApiClient client)
    {
        _logger = logger;
        _client = client;
    }

    public async Task Handle(CheckoutCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request.Basket, _logger);

        _logger.LogInformation("Checking out shopping basket for user ID {UserID}", request.Basket!.Buyer);
        await _client.CheckoutAsync(request.Basket);

        _logger.LogInformation("Succesfully checked out shopping basket for user ID {UserID}", request.Basket!.Buyer);
    }
}
