using Ardalis.Specification;
using AutoMapper;
using AW.Services.SalesOrder.Application.Specifications;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.SalesOrder.Application.GetSalesOrdersForCustomer
{
    public class GetSalesOrdersForCustomerQueryHandler : IRequestHandler<GetSalesOrdersForCustomerQuery, List<SalesOrderDto>>
    {
        private readonly IRepositoryBase<Domain.SalesOrder> repository;
        private readonly IMapper mapper;

        public GetSalesOrdersForCustomerQueryHandler(IRepositoryBase<Domain.SalesOrder> repository, IMapper mapper) =>
            (this.repository, this.mapper) = (repository, mapper);

        public async Task<List<SalesOrderDto>> Handle(GetSalesOrdersForCustomerQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetSalesOrdersForCustomerSpecification(
                request.CustomerNumber
            );

            var orders = await repository.ListAsync(spec);

            return mapper.Map<List<SalesOrderDto>>(orders);
        }
    }
}