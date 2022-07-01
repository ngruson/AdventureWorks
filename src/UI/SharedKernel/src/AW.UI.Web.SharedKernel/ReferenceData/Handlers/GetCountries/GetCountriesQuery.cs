using MediatR;

namespace AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetCountries
{
    public class GetCountriesQuery : IRequest<List<CountryRegion>>
    {
    }
}