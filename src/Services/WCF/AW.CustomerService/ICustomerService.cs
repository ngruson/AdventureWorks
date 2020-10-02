using AW.CustomerService.Messages.GetCustomer;
using AW.CustomerService.Messages.ListCustomers;
using AW.CustomerService.Messages.UpdateCustomer;
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
    }
}