using Ardalis.Specification;
using AutoMapper;
using AW.Services.SalesOrder.Application.Specifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.SalesOrder.Application.GetSalesOrder
{
    public class GetSalesOrderQueryHandler : IRequestHandler<GetSalesOrderQuery, SalesOrderDto>
    {
        private readonly IRepositoryBase<Domain.SalesOrder> repository;
        private readonly IMapper mapper;

        public GetSalesOrderQueryHandler(IRepositoryBase<Domain.SalesOrder> repository, IMapper mapper) =>
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