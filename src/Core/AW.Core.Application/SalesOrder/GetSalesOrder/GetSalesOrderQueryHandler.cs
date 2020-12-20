using Ardalis.Specification;
using AutoMapper;
using AW.Core.Application.Specifications;
using AW.Core.Domain.Sales;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Core.Application.SalesOrder.GetSalesOrder
{
    public class GetSalesOrderQueryHandler : IRequestHandler<GetSalesOrderQuery, SalesOrderDto>
    {
        private readonly IRepositoryBase<SalesOrderHeader> repository;
        private readonly IMapper mapper;

        public GetSalesOrderQueryHandler(IRepositoryBase<SalesOrderHeader> repository, IMapper mapper) =>
            (this.repository, this.mapper) = (repository, mapper);

        public async Task<SalesOrderDto> Handle(GetSalesOrderQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetSalesOrderSpecification(
                request.SalesOrderNumber
            );

            var salesOrder = await repository.GetBySpecAsync(spec);
            return mapper.Map<SalesOrderDto>(salesOrder);
        }
    }
}