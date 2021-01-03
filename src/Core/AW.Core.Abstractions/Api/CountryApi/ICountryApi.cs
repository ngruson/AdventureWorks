using AW.Core.Abstractions.Api.CountryApi.ListCountries;
using System.Threading.Tasks;

namespace AW.Core.Abstractions.Api.CountryApi
{
    public interface ICountryApi
    {
        Task<ListCountriesResponse> ListCountriesAsync();
    }
}