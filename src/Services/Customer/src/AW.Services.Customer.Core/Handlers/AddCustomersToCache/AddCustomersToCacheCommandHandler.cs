using System.Text.Json.Serialization;
using System.Text.Json;
using Ardalis.Result;
using AW.SharedKernel.Caching;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using AW.SharedKernel.JsonConverters;
using AW.Services.Customer.Core.Handlers.GetCustomers;

namespace AW.Services.Customer.Core.Handlers.AddCustomersToCache;

public class AddCustomersToCacheCommandHandler : IRequestHandler<AddCustomersToCacheCommand, Result>
{
    private readonly ILogger<AddCustomersToCacheCommandHandler> _logger;
    private readonly IMediator _mediator;
    private readonly IDistributedCache _cache;
    private readonly CustomerConverter<GetCustomers.Customer, StoreCustomer, IndividualCustomer> _converter;

    public AddCustomersToCacheCommandHandler(
        ILogger<AddCustomersToCacheCommandHandler> logger,
        IMediator mediator,
        IDistributedCache cache,
        CustomerConverter<GetCustomers.Customer, StoreCustomer, IndividualCustomer> converter)
    {
        _logger = logger;
        _mediator = mediator;
        _cache = cache;
        _converter = converter;
    }

    public async Task<Result> Handle(AddCustomersToCacheCommand request, CancellationToken cancellationToken)
    {
        try
        {
            bool force = true;

            var byteArray = await _cache.GetAsync(CacheKeyConstants.AllCustomersKey, cancellationToken);
            if ((force) || byteArray.IsNullOrEmpty())
            {
                _logger.LogInformation("Cache is empty, add customers to cache");

                var customers = await _mediator.Send(
                    new GetCustomersQuery(),
                    cancellationToken
                );

                var options = new JsonSerializerOptions();
                options.Converters.Add(new JsonStringEnumConverter());
                options.Converters.Add(_converter);
                options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

                byteArray = await ConvertData<GetCustomers.Customer>
                    .ObjectListToByteArray(customers, options);
                await _cache.SetAsync(CacheKeyConstants.AllCustomersKey, byteArray, cancellationToken);
            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
            return Result.Error(ex.Message);
        }
    }
}
