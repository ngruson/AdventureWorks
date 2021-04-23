using MediatR;
using System.Collections.Generic;

namespace AW.Services.ReferenceData.Application.CountryRegion.GetCountries
{
    public class GetCountriesQuery : IRequest<List<Country>>
    {
    }
}