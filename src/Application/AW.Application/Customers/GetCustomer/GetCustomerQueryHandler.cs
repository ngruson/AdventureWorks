using AutoMapper;
using AW.Application.Customers;
using AW.Application.Interfaces;
using AW.Application.Specifications;
using AW.Domain.Sales;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.GetCustomer
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerDto>
    {
        private readonly IAsyncRepository<Customer> repository;
        private readonly IMapper mapper;

        public GetCustomerQueryHandler(IAsyncRepository<Customer> repository, IMapper mapper) =>
            (this.repository, this.mapper) = (repository, mapper);

        public async Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetCustomerSpecification(
                request.AccountNumber
            );

            var customer = await repository.FirstOrDefaultAsync(spec);
            return mapper.Map<CustomerDto>(customer);
        }
    }
}