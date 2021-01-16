using AutoMapper;
using AW.Core.Abstractions.Api.CountryApi;
using AW.Core.Abstractions.Api.CountryApi.ListCountries;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AW.Infrastructure.Api.WCF
{
    public class CountryServiceWCF : ICountryApi
    {
        private readonly ILogger<CountryServiceWCF> logger;
        private readonly IMapper mapper;
        private readonly CountryService.ICountryService countryService;

        public CountryServiceWCF(
            ILogger<CountryServiceWCF> logger,
            IMapper mapper,
            CountryService.ICountryService countryService
        ) => (this.logger, this.mapper, this.countryService) = (logger, mapper, countryService);


        public async Task<ListCountriesResponse> ListCountriesAsync()
        {
            logger.LogInformation("Calling ListCountries operation of Country web service");
            var response = await countryService.ListCountriesAsync();
            logger.LogInformation("ListCountries operation executed succesfully");

            return mapper.Map<ListCountriesResponse>(response);
        }
    }
}