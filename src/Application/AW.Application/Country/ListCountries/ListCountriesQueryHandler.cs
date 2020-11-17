using Ardalis.Specification;
using AW.Application.Exceptions;
using AW.Domain.Person;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.Country.ListCountries
{
    public class ListCountriesQueryHandler : IRequestHandler<ListCountriesQuery, IEnumerable<CountryDto>>
    {
        private readonly IRepositoryBase<CountryRegion> repository;

        public ListCountriesQueryHandler(IRepositoryBase<CountryRegion> repository)
            => this.repository = repository;

        public async Task<IEnumerable<CountryDto>> Handle(ListCountriesQuery request, CancellationToken cancellationToken)
        {
            var countries = await repository.ListAsync();
            if (countries.Count == 0)
                throw new CountriesNotFoundException();

            return countries.Select(c => new CountryDto
            {
                CountryRegionCode = c.CountryRegionCode,
                Name = c.Name
            });
        }
    }
}