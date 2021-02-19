using MediatR;
using System.Collections.Generic;

namespace AW.Services.CountryRegion.Application.GetCountries
{
    public class GetCountriesQuery : IRequest<List<CountryDto>>
    {
    }
}