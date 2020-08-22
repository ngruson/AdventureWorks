using AW.Application.Interfaces;
using AW.Application.Specifications;
using AW.Domain.Sales;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.CountSalesOrders
{
    public class CountSalesOrdersQueryHandler : IRequestHandler<CountSalesOrdersQuery, int>
    {
        private readonly IAsyncRepository<SalesOrderHeader> repository;

        public CountSalesOrdersQueryHandler(IAsyncRepository<SalesOrderHeader> repository) =>
            this.repository = repository;

        public async Task<int> Handle(CountSalesOrdersQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetSalesOrdersSpecification(
                request.CustomerType,
                request.Territory
            );

            var salesOrders = await repository.ListAsync(spec);
            return salesOrders.Count;
        }
    }
}