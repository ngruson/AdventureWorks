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
using System.Threading.Tasks;

namespace AW.Core.Abstractions.Api.CustomerApi
{
    public interface ICustomerApi
    {
        Task<ListCustomersResponse> ListCustomersAsync(ListCustomersRequest request);
        Task<GetCustomerResponse> GetCustomerAsync(GetCustomerRequest request);
        Task UpdateCustomerAsync(UpdateCustomerRequest request);
        Task AddCustomerAddressAsync(AddCustomerAddressRequest request);
        Task UpdateCustomerAddressAsync(UpdateCustomerAddressRequest request);
        Task DeleteCustomerAddressAsync(DeleteCustomerAddressRequest request);
        Task AddCustomerContactAsync(AddCustomerContactRequest request);
        Task UpdateCustomerContactAsync(UpdateCustomerContactRequest request);
        Task DeleteCustomerContactAsync(DeleteCustomerContactRequest request);
        Task AddCustomerContactInfoAsync(AddCustomerContactInfoRequest request);
        Task DeleteCustomerContactInfoAsync(DeleteCustomerContactInfoRequest request);
    }
}