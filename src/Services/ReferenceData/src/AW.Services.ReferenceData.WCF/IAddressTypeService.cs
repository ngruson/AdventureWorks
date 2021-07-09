using AW.Services.ReferenceData.WCF.Messages.ListAddressTypes;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.Services.ReferenceData.WCF
{
    [ServiceContract(Namespace = "http://services.aw.com/AddressTypeService/1.0")]
    [XmlSerializerFormat]
    public interface IAddressTypeService
    {

        [OperationContract(Action = "ListAddressTypes", ReplyAction = "ListAddressTypes")]
        Task<ListAddressTypesResponse> ListAddressTypes();
    }
}