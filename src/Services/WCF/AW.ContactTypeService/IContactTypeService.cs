using AW.ContactTypeService.Messages.ListContactTypes;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.ContactTypeService
{
    [ServiceContract(Namespace = "http://services.aw.com/ContactTypeService/1.0")]
    [XmlSerializerFormat]
    public interface IContactTypeService
    {

        [OperationContract(Action = "ListContactTypes", ReplyAction = "ListContactTypes")]
        Task<ListContactTypesResponse> ListContactTypes();
    }
}