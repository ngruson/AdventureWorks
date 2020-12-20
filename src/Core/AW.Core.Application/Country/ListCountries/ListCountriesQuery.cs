using MediatR;
using System.Collections.Generic;

namespace AW.Core.Application.Country.ListCountries
{
    public class ListCountriesQuery : IRequest<IEnumerable<CountryDto>>
    {
    }
}