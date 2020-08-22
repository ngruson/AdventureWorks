using AutoMapper;
using AW.Application.Interfaces;
using AW.Application.Specifications;
using AW.Domain.Sales;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.GetSalesOrders
{
    public class GetSalesOrdersQueryHandler : IRequestHandler<GetSalesOrdersQuery, IEnumerable<SalesOrderDto>>
    {
        private readonly IAsyncRepository<SalesOrderHeader> repository;
        private readonly IMapper mapper;

        public GetSalesOrdersQueryHandler(IAsyncRepository<SalesOrderHeader> repository, IMapper mapper) =>
            (this.repository, this.mapper) = (repository, mapper);

        public async Task<IEnumerable<SalesOrderDto>> Handle(GetSalesOrdersQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetSalesOrdersPaginatedSpecification(
                request.PageIndex,
                request.PageSize,
                request.CustomerType,
                request.Territory
            );

            var orders = await repository.ListAsync(spec);
            return mapper.Map<IEnumerable<SalesOrderDto>>(orders);
        }
    }
}