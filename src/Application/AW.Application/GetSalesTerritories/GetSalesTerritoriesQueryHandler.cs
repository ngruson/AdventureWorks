using AutoMapper;
using AW.Application.Interfaces;
using AW.Domain.Sales;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.GetSalesTerritories
{
    public class GetSalesTerritoriesQueryHandler : IRequestHandler<GetSalesTerritoriesQuery, IEnumerable<TerritoryDto>>
    {
        private readonly IAsyncRepository<SalesTerritory> repository;
        private readonly IMapper mapper;

        public GetSalesTerritoriesQueryHandler(IAsyncRepository<SalesTerritory> repository, IMapper mapper)
            => (this.repository, this.mapper) = (repository, mapper);

        public async Task<IEnumerable<TerritoryDto>> Handle(GetSalesTerritoriesQuery request, CancellationToken cancellationToken)
        {
            var territories = await repository.ListAllAsync();
            return mapper.Map<IEnumerable<TerritoryDto>>(territories);
        }
    }
}