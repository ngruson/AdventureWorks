using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.CustomerService
{
    [ServiceContract]
    public interface ICustomerService
    {

        [OperationContract(Action = "ListCustomers", ReplyAction = "ListCustomers")]
        Task<ListCustomersResponse> ListCustomers(ListCustomersRequest request);
    }
}