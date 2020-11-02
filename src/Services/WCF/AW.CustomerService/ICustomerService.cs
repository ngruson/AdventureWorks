using AW.CustomerService.Messages.AddCustomerAddress;
using AW.CustomerService.Messages.AddCustomerContact;
using AW.CustomerService.Messages.AddCustomerContactInfo;
using AW.CustomerService.Messages.DeleteCustomerAddress;
using AW.CustomerService.Messages.DeleteCustomerContact;
using AW.CustomerService.Messages.DeleteCustomerContactInfo;
using AW.CustomerService.Messages.GetCustomer;
using AW.CustomerService.Messages.ListCustomers;
using AW.CustomerService.Messages.UpdateCustomer;
using AW.CustomerService.Messages.UpdateCustomerAddress;
using AW.CustomerService.Messages.UpdateCustomerContact;
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

        [OperationContract(Action = "AddCustomerContact", ReplyAction = "AddCustomerContact")]
        Task<AddCustomerContactResponse> AddCustomerContact(AddCustomerContactRequest request);

        [OperationContract(Action = "UpdateCustomerContact", ReplyAction = "UpdateCustomerContact")]
        Task<UpdateCustomerContactResponse> UpdateCustomerContact(UpdateCustomerContactRequest request);

        [OperationContract(Action = "DeleteCustomerContact", ReplyAction = "DeleteCustomerContact")]
        Task<DeleteCustomerContactResponse> DeleteCustomerContact(DeleteCustomerContactRequest request);

        [OperationContract(Action = "AddCustomerContactInfo", ReplyAction = "AddCustomerContactInfo")]
        Task<AddCustomerContactInfoResponse> AddCustomerContactInfo(AddCustomerContactInfoRequest request);

        [OperationContract(Action = "DeleteCustomerContactInfo", ReplyAction = "DeleteCustomerContactInfo")]
        Task<DeleteCustomerContactInfoResponse> DeleteCustomerContactInfo(DeleteCustomerContactInfoRequest request);
    }
}