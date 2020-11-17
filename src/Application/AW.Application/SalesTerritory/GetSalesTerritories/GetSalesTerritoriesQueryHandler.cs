using Ardalis.Specification;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.SalesTerritory.GetSalesTerritories
{
    public class GetSalesTerritoriesQueryHandler : IRequestHandler<GetSalesTerritoriesQuery, IEnumerable<TerritoryDto>>
    {
        private readonly IRepositoryBase<Domain.Sales.SalesTerritory> repository;
        private readonly IMapper mapper;

        public GetSalesTerritoriesQueryHandler(IRepositoryBase<Domain.Sales.SalesTerritory> repository, IMapper mapper)
            => (this.repository, this.mapper) = (repository, mapper);

        public async Task<IEnumerable<TerritoryDto>> Handle(GetSalesTerritoriesQuery request, CancellationToken cancellationToken)
        {
            var territories = await repository.ListAsync();
            return mapper.Map<IEnumerable<TerritoryDto>>(territories);
        }
    }
}