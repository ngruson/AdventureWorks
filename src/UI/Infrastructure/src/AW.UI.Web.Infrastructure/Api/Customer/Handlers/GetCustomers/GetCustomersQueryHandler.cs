using System.Text.Json;
using System.Text.Json.Serialization;
using Ardalis.GuardClauses;
using AW.SharedKernel.Caching;
using AW.SharedKernel.Extensions;
using AW.SharedKernel.JsonConverters;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetCustomers;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<Customer?>?>
{
    private readonly ILogger<GetCustomersQueryHandler> _logger;
    private readonly ICustomerApiClient _client;
    private readonly IDistributedCache _cache;
    private readonly CustomerConverter<Customer, StoreCustomer, IndividualCustomer> _converter;

    public GetCustomersQueryHandler(
        ILogger<GetCustomersQueryHandler> logger, 
        ICustomerApiClient client,
        IDistributedCache cache,
        CustomerConverter<Customer, StoreCustomer, IndividualCustomer> converter
    )
    {
        _logger = logger;
        _client = client;
        _cache = cache;
        _converter = converter;
    }

    public async Task<List<Customer?>?> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        List<Customer?>? customers;

        var options = new JsonSerializerOptions();
        options.Converters.Add(new JsonStringEnumConverter());
        options.Converters.Add(_converter);
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

        var byteArray = await _cache.GetAsync(
            CacheKeyConstants.AllCustomersKey,
            cancellationToken
        );
        if (byteArray.IsNullOrEmpty())
        {
            _logger.LogInformation("Cache is empty, calling API to get customers");

            customers = await _client.GetCustomersAsync();
            Guard.Against.Null(customers, _logger);

            byteArray = await ConvertData<Customer>.ObjectListToByteArray(customers!, options);
            await _cache.SetAsync(CacheKeyConstants.AllCustomersKey, byteArray, cancellationToken);
        }
        else
        {
            _logger.LogInformation("Customers are cached");

            customers = await ConvertData<Customer>
                .ByteArrayToObjectList(byteArray!, options)
                .ToListAsync(cancellationToken);

            var cust = customers.Single(_ => _.AccountNumber == "AW00011000");
        }

        _logger.LogInformation("Returning customers for {@Query}", request);

        return customers;
    }
}
