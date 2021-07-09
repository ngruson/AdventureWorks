using MediatR;
using System.Collections.Generic;

namespace AW.Services.ReferenceData.Core.Handlers.CountryRegion.GetCountries
{
    public class GetCountriesQuery : IRequest<List<Country>>
    {
    }
}