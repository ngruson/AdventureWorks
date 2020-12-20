using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using AW.Core.Abstractions.Api.CustomerApi;
using AW.Core.Abstractions.Api.CustomerApi.AddCustomerAddress;
using AW.Core.Abstractions.Api.CustomerApi.AddCustomerContact;
using AW.Core.Abstractions.Api.CustomerApi.AddCustomerContactInfo;
using AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerAddress;
using AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerContact;
using AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerContactInfo;
using AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using AW.Core.Abstractions.Api.CustomerApi.ListCustomers;
using AW.Core.Abstractions.Api.CustomerApi.UpdateCustomer;
using AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerAddress;
using AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerContact;

namespace AW.Infrastructure.Api.WCF
{
    public class CustomerServiceAdapter : ICustomerApi
    {        
        private readonly IMapper mapper;
        private readonly ILogger<CustomerServiceAdapter> logger;
        private readonly CustomerService.ICustomerService customerService;

        public CustomerServiceAdapter(ILogger<CustomerServiceAdapter> logger, IMapper mapper, CustomerService.ICustomerService customerService)
            => (this.logger, this.mapper, this.customerService) = (logger, mapper, customerService);

        public async Task AddCustomerAddressAsync(AddCustomerAddressRequest request)
        {
            logger.LogInformation("Mapping to AddCustomerAddressRequest");
            var req = mapper.Map<CustomerService.AddCustomerAddressRequest>(request);

            logger.LogInformation("Calling AddCustomerAddress operation of Customer web service");
            await customerService.AddCustomerAddressAsync(req);
            logger.LogInformation("AddCustomerAddress operation executed succesfully");
        }

        public async Task AddCustomerContactAsync(AddCustomerContactRequest request)
        {
            logger.LogInformation("Mapping to AddCustomerContactRequest");
            var req = mapper.Map<CustomerService.AddCustomerContactRequest>(request);

            logger.LogInformation("Calling AddCustomerContact operation of Customer web service");
            await customerService.AddCustomerContactAsync(req);
            logger.LogInformation("AddCustomerContact operation executed succesfully");
        }

        public async Task AddCustomerContactInfoAsync(AddCustomerContactInfoRequest request)
        {
            logger.LogInformation("Mapping to AddCustomerContactInfoRequest");
            var req = mapper.Map<CustomerService.AddCustomerContactInfoRequest>(request);

            logger.LogInformation("Calling AddCustomerContactInfo operation of Customer web service");
            await customerService.AddCustomerContactInfoAsync(req);
            logger.LogInformation("AddCustomerContactInfo operation executed succesfully");
        }

        public async Task DeleteCustomerAddressAsync(DeleteCustomerAddressRequest request)
        {
            logger.LogInformation("Mapping to DeleteCustomerAddressRequest");
            var req = mapper.Map<CustomerService.DeleteCustomerAddressRequest>(request);

            logger.LogInformation("Calling DeleteCustomerAddress operation of Customer web service");
            await customerService.DeleteCustomerAddressAsync(req);
            logger.LogInformation("DeleteCustomerAddress operation executed succesfully");
        }

        public async Task DeleteCustomerContactAsync(DeleteCustomerContactRequest request)
        {
            logger.LogInformation("Mapping to DeleteCustomerContactRequest");
            var req = mapper.Map<CustomerService.DeleteCustomerContactRequest>(request);

            logger.LogInformation("Calling DeleteCustomerContact operation of Customer web service");
            await customerService.DeleteCustomerContactAsync(req);
            logger.LogInformation("DeleteCustomerContact operation executed succesfully");
        }

        public async Task DeleteCustomerContactInfoAsync(DeleteCustomerContactInfoRequest request)
        {
            logger.LogInformation("Mapping to DeleteCustomerContactInfoRequest");
            var req = mapper.Map<CustomerService.DeleteCustomerContactInfoRequest>(request);

            logger.LogInformation("Calling DeleteCustomerContactInfo operation of Customer web service");
            await customerService.DeleteCustomerContactInfoAsync(req);
            logger.LogInformation("DeleteCustomerContactInfo operation executed succesfully");
        }

        public async Task<GetCustomerResponse> GetCustomerAsync(GetCustomerRequest request)
        {
            logger.LogInformation("Mapping to GetCustomerRequest");
            var req = mapper.Map<CustomerService.GetCustomerRequest>(request);

            logger.LogInformation("Calling GetCustomer operation of Customer web service");
            var response = await customerService.GetCustomerAsync(req);
            logger.LogInformation("GetCustomer operation executed succesfully");

            return mapper.Map<GetCustomerResponse>(response);
        }

        public async Task<ListCustomersResponse> ListCustomersAsync(ListCustomersRequest request)
        {
            logger.LogInformation("Mapping to ListCustomersRequest");
            var req = mapper.Map<CustomerService.ListCustomersRequest>(request);

            logger.LogInformation("Calling ListCustomers operation of Customer web service");
            var response = await customerService.ListCustomersAsync(req);
            logger.LogInformation("ListCustomers operation executed succesfully");

            return mapper.Map<ListCustomersResponse>(response);
        }

        public async Task UpdateCustomerAsync(UpdateCustomerRequest request)
        {
            logger.LogInformation("Mapping to UpdateCustomerRequest");
            var req = mapper.Map<CustomerService.UpdateCustomerRequest>(request);

            logger.LogInformation("Calling UpdateCustomer operation of Customer web service");
            await customerService.UpdateCustomerAsync(req);
            logger.LogInformation("UpdateCustomer operation executed succesfully");
        }

        public async Task UpdateCustomerAddressAsync(UpdateCustomerAddressRequest request)
        {
            logger.LogInformation("Mapping to UpdateCustomerAddressRequest");
            var req = mapper.Map<CustomerService.UpdateCustomerAddressRequest>(request);

            logger.LogInformation("Calling UpdateCustomerAddress operation of Customer web service");
            await customerService.UpdateCustomerAddressAsync(req);
            logger.LogInformation("UpdateCustomerAddress operation executed succesfully");
        }

        public async Task UpdateCustomerContactAsync(UpdateCustomerContactRequest request)
        {
            logger.LogInformation("Mapping to UpdateCustomerContactRequest");
            var req = mapper.Map<CustomerService.UpdateCustomerContactRequest>(request);

            logger.LogInformation("Calling UpdateCustomerContact operation of Customer web service");
            await customerService.UpdateCustomerContactAsync(req);
            logger.LogInformation("UpdateCustomerContact operation executed succesfully");
        }
    }
}