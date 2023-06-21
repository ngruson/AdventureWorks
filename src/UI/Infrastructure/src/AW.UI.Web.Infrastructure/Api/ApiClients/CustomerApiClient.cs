using AW.SharedKernel.Interfaces;
using AW.SharedKernel.JsonConverters;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using GetCustomer = AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetCustomer;
using GetCustomers = AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetCustomers;
using UpdateCustomer = AW.UI.Web.Infrastructure.Api.Customer.Handlers.UpdateCustomer;

namespace AW.UI.Web.Infrastructure.Api.ApiClients;

public class CustomerApiClient : ICustomerApiClient
{
    private readonly HttpClient _client;
    private readonly ILogger<CustomerApiClient> _logger;

    private readonly CustomerConverter<
        GetCustomers.Customer,
        GetCustomers.StoreCustomer,
        GetCustomers.IndividualCustomer> _converterGetCustomers;

    private readonly CustomerConverter<
        GetCustomer.Customer,
        GetCustomer.StoreCustomer,
        GetCustomer.IndividualCustomer> _converterGetCustomer;

    private readonly CustomerConverter<
        UpdateCustomer.Customer,
        UpdateCustomer.StoreCustomer,
        UpdateCustomer.IndividualCustomer> _converterUpdateCustomer;

    public CustomerApiClient(
        HttpClient client,
        ILogger<CustomerApiClient> logger,
        CustomerConverter<GetCustomers.Customer, GetCustomers.StoreCustomer, GetCustomers.IndividualCustomer> converterGetCustomers,
        CustomerConverter<GetCustomer.Customer, GetCustomer.StoreCustomer, GetCustomer.IndividualCustomer> converterGetCustomer,
        CustomerConverter<UpdateCustomer.Customer, UpdateCustomer.StoreCustomer, UpdateCustomer.IndividualCustomer> converterUpdateCustomer
    ) => (_client, _logger, _converterGetCustomers, _converterGetCustomer, _converterUpdateCustomer) = (client, logger, converterGetCustomers, converterGetCustomer, converterUpdateCustomer);

    public async Task<List<GetCustomers.Customer?>?> GetCustomersAsync(
        CustomerType? customerType
    )
    {
        var requestUri = $"Customer?api-version=1.0";

        if (customerType.HasValue)
        {
            var customerTypeValue = customerType.Value == CustomerType.Individual ? 0 : 1;
            requestUri += $"&customerType={customerTypeValue}";
            _logger.LogInformation("Getting customers with customer type {CustomerType}", customerTypeValue);
        }
        else
            _logger.LogInformation("Getting customers");

        using var response = await _client.GetAsync(requestUri);

        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();

        return await stream.DeserializeAsync<List<GetCustomers.Customer?>>(
            new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter(),
                    _converterGetCustomers
                },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }
        );
    }

    public async Task<GetCustomer.Customer?> GetCustomerAsync(Guid objectId)
    {
        return await GetCustomerAsync<GetCustomer.Customer?>(objectId);
    }

    public async Task<T?> GetCustomerAsync<T>(Guid objectId)
    {
        _logger.LogInformation("Getting customer {ObjectId}", objectId);

        try
        {
            using var response = await _client.GetAsync($"Customer/{objectId}?&api-version=1.0");
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<T?>(
                new JsonSerializerOptions
                {
                    Converters =
                    {
                        new JsonStringEnumConverter(),
                        _converterGetCustomer
                    },
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );
        }
        catch (Exception ex)
        {
            _logger.LogError("Getting customer {ObjectId} failed", objectId);
            throw new CustomerApiClientException($"Getting customer {objectId} failed", ex);
        }
    }

    public async Task<Customer.Handlers.GetPreferredAddress.Address?> GetPreferredAddressAsync(string? accountNumber, string? addressType)
    {
        _logger.LogInformation("Getting preferred address for address type {AddressType} for customer {AccountNumber}", addressType, accountNumber);

        try
        {
            using var response = await _client.GetAsync($"Customer/{accountNumber}/preferredAddress/{addressType}?&api-version=1.0");
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<Customer.Handlers.GetPreferredAddress.Address?>(
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );
        }
        catch (Exception ex)
        {
            _logger.LogError("Getting preferred address for address type {AddressType} for customer {AccountNumber} failed", addressType, accountNumber);
            throw new CustomerApiClientException($"Getting preferred address for address type {addressType} for customer {accountNumber} failed", ex);
        }
    }

    public async Task<UpdateCustomer.Customer?> UpdateCustomerAsync(UpdateCustomer.Customer customer)
    {
        _logger.LogInformation("Updating customer {ObjectId}", customer.ObjectId);
        var requestUri = $"Customer/{customer.ObjectId}?&api-version=1.0";

        var options = new JsonSerializerOptions
        {
            Converters =
                {
                    new JsonStringEnumConverter(),
                    _converterUpdateCustomer
                },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var json = JsonSerializer.Serialize(customer, options);
        _logger.LogInformation("Calling PUT method on {RequestUri} with {JSON}", requestUri, json);

        using var response = await _client.PutAsync(
            requestUri,
            new StringContent(json, Encoding.UTF8, "application/json")
        );
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();
        var updatedCustomer = await stream.DeserializeAsync<UpdateCustomer.Customer>(
            options
        );

        _logger.LogInformation("Returning customer {@Customer}", updatedCustomer);
        return updatedCustomer;
    }
}
