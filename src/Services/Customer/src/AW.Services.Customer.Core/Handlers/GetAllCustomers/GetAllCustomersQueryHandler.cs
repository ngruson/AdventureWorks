using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Customer.Core.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Core.Handlers.GetAllCustomers
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerDto>>
    {
        private readonly ILogger<GetAllCustomersQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.Customer> _repository;

        public GetAllCustomersQueryHandler(
            ILogger<GetAllCustomersQueryHandler> logger,
            IMapper mapper,
            IRepository<Entities.Customer> repository
        ) => (_logger, _mapper, _repository) = (logger, mapper, repository);
        
        public async Task<List<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting customers for request {@Request}", request);

            var spec = new GetAllCustomersSpecification(
                    _mapper.Map<Entities.CustomerType>(request.CustomerType)
                );

            var customers = await _repository.ListAsync(spec, cancellationToken);            
            Guard.Against.CustomersNull(customers, _logger);

            _logger.LogInformation("Returning {Count} customers", customers.Count);

            return _mapper.Map<List<CustomerDto>>(customers);
        }
    }
}