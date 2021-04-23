using AW.Services.ReferenceData.WCF.Messages.ListContactTypes;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.Services.ReferenceData.WCF
{
    [ServiceContract(Namespace = "http://services.aw.com/ContactTypeService/1.0")]
    [XmlSerializerFormat]
    public interface IContactTypeService
    {

        [OperationContract(Action = "ListContactTypes", ReplyAction = "ListContactTypes")]
        Task<ListContactTypesResponse> ListContactTypes();
    }
}