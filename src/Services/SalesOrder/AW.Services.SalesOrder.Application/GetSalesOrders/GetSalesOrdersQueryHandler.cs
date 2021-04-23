using Ardalis.Specification;
using AutoMapper;
using AW.Services.SalesOrder.Application.Specifications;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.SalesOrder.Application.GetSalesOrders
{
    public class GetSalesOrdersQueryHandler : IRequestHandler<GetSalesOrdersQuery, GetSalesOrdersDto>
    {
        private readonly IRepositoryBase<Domain.SalesOrder> repository;
        private readonly IMapper mapper;

        public GetSalesOrdersQueryHandler(IRepositoryBase<Domain.SalesOrder> repository, IMapper mapper) =>
            (this.repository, this.mapper) = (repository, mapper);

        public async Task<GetSalesOrdersDto> Handle(GetSalesOrdersQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetSalesOrdersPaginatedSpecification(
                request.PageIndex,
                request.PageSize,
                request.CustomerType,
                request.Territory
            );
            var countSpec = new CountSalesOrdersSpecification(
                request.CustomerType,
                request.Territory
            );

            var orders = await repository.ListAsync(spec);

            return new GetSalesOrdersDto
            {
                SalesOrders = mapper.Map<IEnumerable<SalesOrderDto>>(orders),
                TotalSalesOrders = await repository.CountAsync(countSpec)
            };
        }
    }
}