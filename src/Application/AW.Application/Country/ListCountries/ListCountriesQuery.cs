using MediatR;
using System.Collections.Generic;

namespace AW.Application.Country.ListCountries
{
    public class ListCountriesQuery : IRequest<IEnumerable<CountryDto>>
    {
    }
}