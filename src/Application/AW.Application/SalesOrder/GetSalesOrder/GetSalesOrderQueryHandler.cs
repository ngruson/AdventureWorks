using AutoMapper;
using AW.Application.Interfaces;
using AW.Application.Specifications;
using AW.Domain.Sales;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.SalesOrder.GetSalesOrder
{
    public class GetSalesOrderQueryHandler : IRequestHandler<GetSalesOrderQuery, SalesOrderDto>
    {
        private readonly IAsyncRepository<SalesOrderHeader> repository;
        private readonly IMapper mapper;

        public GetSalesOrderQueryHandler(IAsyncRepository<SalesOrderHeader> repository, IMapper mapper) =>
            (this.repository, this.mapper) = (repository, mapper);

        public async Task<SalesOrderDto> Handle(GetSalesOrderQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetSalesOrderSpecification(
                request.SalesOrderNumber
            );

            var salesOrder = await repository.FirstOrDefaultAsync(spec);
            return mapper.Map<SalesOrderDto>(salesOrder);
        }
    }
}