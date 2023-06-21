using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetStoreCustomer;

public class GetStoreCustomerQueryHandler : IRequestHandler<GetStoreCustomerQuery, StoreCustomer?>
{
    private readonly ILogger<GetStoreCustomerQueryHandler> _logger;
    private readonly ICustomerApiClient _client;

    public GetStoreCustomerQueryHandler(ILogger<GetStoreCustomerQueryHandler> logger, ICustomerApiClient client)
    {
        _logger = logger;
        _client = client;
    }

    public async Task<StoreCustomer?> Handle(GetStoreCustomerQuery request, CancellationToken cancellationToken)
    {
        Guard.Against.NullOrEmpty(request.ObjectId, _logger);

        _logger.LogInformation("Getting store customer {ObjectId} from API", request.ObjectId);
        var customer = await _client.GetCustomerAsync<StoreCustomer>(
            request.ObjectId
        );
        Guard.Against.Null(customer, _logger);

        _logger.LogInformation("Returning store customer {ObjectId}", request.ObjectId);

        return customer!;
    }
}
