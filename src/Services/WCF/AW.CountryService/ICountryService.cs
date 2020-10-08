using AW.CountryService.Messages.ListCountries;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.CountryService
{
    [ServiceContract(Namespace = "http://services.aw.com/CountryService/1.0")]
    [XmlSerializerFormat]
    public interface ICountryService
    {
        [OperationContract(Action = "ListCountries", ReplyAction = "ListCountries")]
        Task<ListCountriesResponse> ListCountries();
    }
}