using AutoMapper;
using AW.Application.Interfaces;
using AW.Application.Specifications;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.SalesPerson.GetSalesPersons
{
    public class GetSalesPersonsQueryHandler : IRequestHandler<GetSalesPersonsQuery, IEnumerable<SalesPersonDto>>
    {
        private readonly IAsyncRepository<Domain.Sales.SalesPerson> repository;
        private readonly IMapper mapper;

        public GetSalesPersonsQueryHandler(IAsyncRepository<Domain.Sales.SalesPerson> repository, IMapper mapper)
            => (this.repository, this.mapper) = (repository, mapper);

        public async Task<IEnumerable<SalesPersonDto>> Handle(GetSalesPersonsQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.SalesTerritoryName))
            {
                var spec = new GetSalesPersonsSpecification(request.SalesTerritoryName);
                var salesPersons = await repository.ListAsync(spec);
                return mapper.Map<IEnumerable<SalesPersonDto>>(salesPersons);
            }
            else
            {
                var salesPersons = await repository.ListAllAsync();
                return mapper.Map<IEnumerable<SalesPersonDto>>(salesPersons);
            }
        }
    }
}