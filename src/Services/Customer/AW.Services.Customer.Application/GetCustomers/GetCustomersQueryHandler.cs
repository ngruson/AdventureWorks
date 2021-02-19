using Ardalis.GuardClauses;
using Ardalis.Specification;
using AutoMapper;
using AW.Services.Customer.Application.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Application.GetCustomers
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomersQuery, GetCustomersDto>
    {
        private readonly ILogger<GetCustomerQueryHandler> logger;
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly IRepositoryBase<Domain.Customer> repository;

        public GetCustomerQueryHandler(
            ILogger<GetCustomerQueryHandler> logger,
            IMediator mediator,
            IMapper mapper,
            IRepositoryBase<Domain.Customer> repository
        ) => (this.logger, this.mediator, this.mapper, this.repository) = (logger, mediator, mapper, repository);

        public async Task<GetCustomersDto> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");
            logger.LogInformation("Getting customers from database");

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
            Guard.Against.Null(customers, nameof(customers));

            logger.LogInformation("Returning customers");
            return new GetCustomersDto
            {
                Customers = mapper.Map<List<CustomerDto>>(customers),
                TotalCustomers = await repository.CountAsync(countSpec)
            };
        }
    }
}