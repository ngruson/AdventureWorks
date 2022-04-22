using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Core.Handlers.GetAllCustomers
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerDto>>
    {
        private readonly ILogger<GetAllCustomersQueryHandler> logger;
        private readonly IMapper mapper;
        private readonly IRepository<Entities.Customer> repository;

        public GetAllCustomersQueryHandler(
            ILogger<GetAllCustomersQueryHandler> logger,
            IMapper mapper,
            IRepository<Entities.Customer> repository
        ) => (this.logger, this.mapper, this.repository) = (logger, mapper, repository);
        
        public async Task<List<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting customers for request {@Request}", request);

            var spec = new GetAllCustomersSpecification(
                    mapper.Map<Entities.CustomerType>(request.CustomerType)
                );

            var customers = await repository.ListAsync(spec, cancellationToken);            
            Guard.Against.Null(customers, nameof(customers));

            logger.LogInformation("Returning {Count} customers", customers.Count);

            return mapper.Map<List<CustomerDto>>(customers);
        }
    }
}