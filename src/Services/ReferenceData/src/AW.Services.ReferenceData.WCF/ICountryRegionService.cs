using AW.Services.ReferenceData.WCF.Messages.ListCountries;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.Services.ReferenceData.WCF
{
    [ServiceContract(Namespace = "http://services.aw.com/CountryService/1.0")]
    [XmlSerializerFormat]
    public interface ICountryRegionService
    {
        [OperationContract(Action = "ListCountries", ReplyAction = "ListCountries")]
        Task<ListCountriesResponse> ListCountries();
    }
}