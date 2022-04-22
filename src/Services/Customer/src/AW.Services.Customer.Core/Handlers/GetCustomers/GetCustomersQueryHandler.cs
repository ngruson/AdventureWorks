using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Customer.Core.Handlers.GetCustomer;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Core.Handlers.GetCustomers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, GetCustomersDto>
    {
        private readonly ILogger<GetCustomersQueryHandler> logger;
        private readonly IMapper mapper;
        private readonly IRepository<Entities.Customer> repository;

        public GetCustomersQueryHandler(
            ILogger<GetCustomersQueryHandler> logger,
            IMapper mapper,
            IRepository<Entities.Customer> repository
        ) => (this.logger, this.mapper, this.repository) = (logger, mapper, repository);

        public async Task<GetCustomersDto> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");
            logger.LogInformation("Getting customers from database");

            var spec = new GetCustomersPaginatedSpecification(
                request.PageIndex,
                request.PageSize,
                mapper.Map<Entities.CustomerType?>(request.CustomerType),
                request.Territory,
                request.AccountNumber
            );
            var countSpec = new CountCustomersSpecification(
                mapper.Map<Entities.CustomerType?>(request.CustomerType),
                request.Territory,
                request.AccountNumber
            );

            var customers = await repository.ListAsync(spec, cancellationToken);
            Guard.Against.Null(customers, nameof(customers));

            logger.LogInformation("Returning customers");
            return new GetCustomersDto
            {
                Customers = mapper.Map<List<CustomerDto>>(customers),
                TotalCustomers = await repository.CountAsync(countSpec, cancellationToken)
            };
        }
    }
}