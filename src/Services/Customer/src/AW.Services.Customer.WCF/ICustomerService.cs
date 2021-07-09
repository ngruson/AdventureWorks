using AW.Services.Customer.WCF.Messages.AddCustomerAddress;
using AW.Services.Customer.WCF.Messages.AddIndividualCustomerEmailAddress;
using AW.Services.Customer.WCF.Messages.AddStoreCustomerContact;
using AW.Services.Customer.WCF.Messages.DeleteCustomerAddress;
using AW.Services.Customer.WCF.Messages.DeleteIndividualCustomerEmailAddress;
using AW.Services.Customer.WCF.Messages.DeleteStoreCustomerContact;
using AW.Services.Customer.WCF.Messages.GetCustomer;
using AW.Services.Customer.WCF.Messages.ListCustomers;
using AW.Services.Customer.WCF.Messages.UpdateCustomer;
using AW.Services.Customer.WCF.Messages.UpdateCustomerAddress;
using AW.Services.Customer.WCF.Messages.UpdateStoreCustomerContact;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.Services.Customer.WCF
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

        [OperationContract(Action = "AddStoreCustomerContact", ReplyAction = "AddStoreCustomerContact")]
        Task<AddStoreCustomerContactResponse> AddStoreCustomerContact(AddStoreCustomerContactRequest request);

        [OperationContract(Action = "UpdateStoreCustomerContact", ReplyAction = "UpdateStoreCustomerContact")]
        Task<UpdateStoreCustomerContactResponse> UpdateStoreCustomerContact(UpdateStoreCustomerContactRequest request);

        [OperationContract(Action = "DeleteStoreCustomerContact", ReplyAction = "DeleteStoreCustomerContact")]
        Task<DeleteStoreCustomerContactResponse> DeleteStoreCustomerContact(DeleteStoreCustomerContactRequest request);

        [OperationContract(Action = "AddIndividualCustomerEmailAddress", ReplyAction = "AddIndividualCustomerEmailAddress")]
        Task<AddIndividualCustomerEmailAddressResponse> AddIndividualCustomerEmailAddress(AddIndividualCustomerEmailAddressRequest request);

        [OperationContract(Action = "DeleteIndividualCustomerEmailAddress", ReplyAction = "DeleteIndividualCustomerEmailAddress")]
        Task<DeleteIndividualCustomerEmailAddressResponse> DeleteIndividualCustomerEmailAddress(DeleteIndividualCustomerEmailAddressRequest request);
    }
}