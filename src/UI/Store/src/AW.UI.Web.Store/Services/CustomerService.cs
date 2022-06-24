using AW.UI.Web.Infrastructure.ApiClients.CustomerApi;
using AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Exceptions;
using models = AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AW.UI.Web.Store.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ILogger<CustomerService> logger;
        private readonly ICustomerApiClient customerApiClient;

        public CustomerService(
            ILogger<CustomerService> logger,
            ICustomerApiClient customerApiClient
        ) => (this.logger, this.customerApiClient) = (logger, customerApiClient);

        public async Task<models.GetCustomer.Customer> GetCustomerAsync(string customerNumber)
        {
            logger.LogInformation("GetCustomerAsync called");

            try
            {
                var customer = await customerApiClient.GetCustomerAsync(customerNumber);
                logger.LogInformation("Getting customer {CustomerNumber} succeeded", customerNumber);
                return customer;
            }
            catch (CustomerApiClientException ex)
            {
                logger.LogError(ex, "Getting customer {CustomerNumber} failed", customerNumber);
                return null;
            }            
        }

        public async Task<models.GetPreferredAddress.Address> GetPreferredAddressAsync(string customerNumber, string addressType)
        {
            logger.LogInformation(
                "GetPreferredAddressAsync called. CustomerNumber = {CustomerNumber}, address type = {AddressType}",
                customerNumber, addressType
            );

            try
            {
                var address = await customerApiClient.GetPreferredAddressAsync(customerNumber, addressType);
                logger.LogInformation("Getting preferred address {@Address} succeeded", address);
                return address;
            }
            catch (CustomerApiClientException ex)
            {
                logger.LogError(ex, "Getting preferred address failed");
                return null;
            }
        }
    }
}