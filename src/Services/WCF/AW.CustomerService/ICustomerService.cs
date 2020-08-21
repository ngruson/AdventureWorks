using AW.CustomerService.Messages;
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
    }
}