using AW.StateProvinceService.Messages.ListStateProvinces;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.StateProvinceService
{
    [ServiceContract(Namespace = "http://services.aw.com/StateProvinceService/1.0")]
    [XmlSerializerFormat]
    public interface IStateProvinceService
    {
        [OperationContract(Action = "ListStateProvinces", ReplyAction = "ListStateProvinces")]
        Task<ListStateProvincesResponse> ListStateProvinces(ListStateProvincesRequest request);
    }
}