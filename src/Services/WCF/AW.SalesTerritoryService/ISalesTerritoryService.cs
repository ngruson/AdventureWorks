using AW.SalesTerritoryService.Messages;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.SalesTerritoryService
{
    [ServiceContract(Namespace = "http://services.aw.com/SalesTerritoryService/1.0")]
    [XmlSerializerFormat]
    public interface ISalesTerritoryService
    {

        [OperationContract(Action = "ListTerritories", ReplyAction = "ListTerritories")]
        Task<ListTerritoriesResponse> ListTerritories();
    }
}