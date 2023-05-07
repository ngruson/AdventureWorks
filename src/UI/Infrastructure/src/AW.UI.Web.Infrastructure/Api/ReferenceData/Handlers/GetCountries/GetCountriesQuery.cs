using MediatR;

namespace AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetCountries
{
    public class GetCountriesQuery : IRequest<List<CountryRegion>>
    {
    }
}