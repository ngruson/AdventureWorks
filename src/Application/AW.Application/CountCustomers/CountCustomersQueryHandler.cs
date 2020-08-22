using AW.Application.Interfaces;
using AW.Application.Specifications;
using AW.Domain.Sales;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.CountCustomers
{
    public class CountCustomersQueryHandler : IRequestHandler<CountCustomersQuery, int>
    {
        private readonly IAsyncRepository<Customer> repository;

        public CountCustomersQueryHandler(IAsyncRepository<Customer> repository) =>
            this.repository = repository;

        public async Task<int> Handle(CountCustomersQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetCustomersSpecification(
                request.CustomerType,
                request.Territory
            );

            var customers = await repository.ListAsync(spec);
            return customers.Count;
        }
    }
}