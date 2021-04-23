using AW.Services.ReferenceData.Application.CountryRegion.GetCountries;
using AW.Services.ReferenceData.WCF.Messages.ListCountries;
using MediatR;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.Services.ReferenceData.WCF
{
    [ServiceBehavior(Namespace = "http://services.aw.com/CountryRegionService/1.0")]
    public class CountryRegionService : ICountryRegionService
    {
        private readonly IMediator mediator;

        public CountryRegionService(IMediator mediator) => this.mediator = mediator;

        public async Task<ListCountriesResponse> ListCountries()
        {
            var countries = await mediator.Send(
                new GetCountriesQuery()
            );

            return new ListCountriesResponse
            {
                Countries = countries
            };
        }
    }
}