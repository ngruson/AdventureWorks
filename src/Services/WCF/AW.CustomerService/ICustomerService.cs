using AW.CustomerService.Messages.AddCustomerAddress;
using AW.CustomerService.Messages.DeleteCustomerAddress;
using AW.CustomerService.Messages.GetCustomer;
using AW.CustomerService.Messages.ListCustomers;
using AW.CustomerService.Messages.UpdateCustomer;
using AW.CustomerService.Messages.UpdateCustomerAddress;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.CustomerService
{
    [ServiceContract(Namespace = "http://services.aw.com/CustomerService/1.0")]
    [XmlSerializerFormat]
    public interface ICustomerService
    {

        [OperationContract(Action = "ListCustomers", ReplyAction = "ListCustomers")]
        Task<ListCustomersResponse> ListCustomers(ListCustomersRequest request);

        [OperationContract(Action = "GetCustomer", ReplyAction = "GetCustomer")]
        Task<GetCustomerResponse> GetCustomer(GetCustomerRequest request);

        [OperationContract(Action = "UpdateCustomer", ReplyAction = "UpdateCustomer")]
        Task<UpdateCustomerResponse> UpdateCustomer(UpdateCustomerRequest request);

        [OperationContract(Action = "AddCustomerAddress", ReplyAction = "AddCustomerAddress")]
        Task<AddCustomerAddressResponse> AddCustomerAddress(AddCustomerAddressRequest request);

        [OperationContract(Action = "UpdateCustomerAddress", ReplyAction = "UpdateCustomerAddress")]
        Task<UpdateCustomerAddressResponse> UpdateCustomerAddress(UpdateCustomerAddressRequest request);

        [OperationContract(Action = "DeleteCustomerAddress", ReplyAction = "DeleteCustomerAddress")]
        Task<DeleteCustomerAddressResponse> DeleteCustomerAddress(DeleteCustomerAddressRequest request);
    }
}