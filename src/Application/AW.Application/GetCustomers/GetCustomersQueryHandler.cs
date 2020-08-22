using AutoMapper;
using AW.Application.Interfaces;
using AW.Application.Specifications;
using AW.Domain.Sales;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.GetCustomers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, IEnumerable<CustomerDto>>
    {
        private readonly IAsyncRepository<Customer> repository;
        private readonly IMapper mapper;

        public GetCustomersQueryHandler(IAsyncRepository<Customer> repository, IMapper mapper) =>
            (this.repository, this.mapper) = (repository, mapper);

        public async Task<IEnumerable<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetCustomersPaginatedSpecification(
                request.PageIndex, 
                request.PageSize, 
                request.CustomerType,
                request.Territory
            );

            var customers = await repository.ListAsync(spec);
            return mapper.Map<IEnumerable<CustomerDto>>(customers);
        }
    }
}