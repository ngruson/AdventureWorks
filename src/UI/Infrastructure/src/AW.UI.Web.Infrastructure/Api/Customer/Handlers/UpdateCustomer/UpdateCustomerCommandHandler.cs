using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.UpdateCustomer;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
{
    private readonly ILogger<UpdateCustomerCommandHandler> _logger;
    private readonly ICustomerApiClient _client;

    public UpdateCustomerCommandHandler(ILogger<UpdateCustomerCommandHandler> logger, ICustomerApiClient client)
    {
        _logger = logger;
        _client = client;
    }

    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating customer {ObjectId}", request.Customer.ObjectId);
        var customer = await _client.UpdateCustomerAsync(
            request.Customer!
        );
        Guard.Against.Null(customer, _logger);

        _logger.LogInformation("Customer was updated successfully");
    }
}
