using AW.AddressTypeService.Messages.ListAddressTypes;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.AddressTypeService
{
    [ServiceContract(Namespace = "http://services.aw.com/AddressTypeService/1.0")]
    [XmlSerializerFormat]
    public interface IAddressTypeService
    {

        [OperationContract(Action = "ListAddressTypes", ReplyAction = "ListAddressTypes")]
        Task<ListAddressTypesResponse> ListAddressTypes();
    }
}