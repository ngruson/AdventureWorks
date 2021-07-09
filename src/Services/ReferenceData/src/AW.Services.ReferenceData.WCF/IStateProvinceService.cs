using AW.Services.ReferenceData.WCF.Messages.ListStatesProvinces;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.Services.ReferenceData.WCF
{
    [ServiceContract(Namespace = "http://services.aw.com/StateProvinceService/1.0")]
    [XmlSerializerFormat]
    public interface IStateProvinceService
    {
        [OperationContract(Action = "ListStatesProvinces", ReplyAction = "ListStatesProvinces")]
        Task<ListStatesProvincesResponse> ListStatesProvinces(ListStatesProvincesRequest request);
    }
}