using Ardalis.Specification;
using AutoMapper;
using AW.Application.Specifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.Customer.GetCustomer
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerDto>
    {
        private readonly IRepositoryBase<Domain.Sales.Customer> repository;
        private readonly IMapper mapper;

        public GetCustomerQueryHandler(IRepositoryBase<Domain.Sales.Customer> repository, IMapper mapper) =>
            (this.repository, this.mapper) = (repository, mapper);

        public async Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetCustomerSpecification(
                request.AccountNumber
            );

            var customer = await repository.GetBySpecAsync(spec);

            return mapper.Map<CustomerDto>(customer);
        }
    }
}