using AW.Application.Country.ListCountries;
using AW.CountryService.Messages.ListCountries;
using MediatR;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.CountryService
{
    [ServiceBehavior(Namespace = "http://services.aw.com/CountryService/1.0")]
    public class CountryService : ICountryService
    {
        private readonly IMediator mediator;

        public CountryService(IMediator mediator) => this.mediator = mediator;
        
        public async Task<ListCountriesResponse> ListCountries()
        {
            var countries = await mediator.Send(
                new ListCountriesQuery()
            );

            return new ListCountriesResponse
            {
                Countries = countries.ToList()
            };
        }
    }
}