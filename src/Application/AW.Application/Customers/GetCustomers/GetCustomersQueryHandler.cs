using AutoMapper;
using AW.Application.Customers;
using AW.Application.Interfaces;
using AW.Application.Specifications;
using AW.Domain.Sales;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.GetCustomers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, GetCustomersDto>
    {
        private readonly IAsyncRepository<Customer> repository;
        private readonly IMapper mapper;

        public GetCustomersQueryHandler(IAsyncRepository<Customer> repository, IMapper mapper) =>
            (this.repository, this.mapper) = (repository, mapper);

        public async Task<GetCustomersDto> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetCustomersPaginatedSpecification(
                request.PageIndex, 
                request.PageSize, 
                request.CustomerType,
                request.Territory
            );
            var countSpec = new CountCustomersSpecification(
                request.CustomerType, 
                request.Territory
            );

            var customers = await repository.ListAsync(spec);

            return new GetCustomersDto
            {
                Customers = mapper.Map<IEnumerable<CustomerDto>>(customers),
                TotalCustomers = await repository.CountAsync(countSpec)
            };
        }
    }
}