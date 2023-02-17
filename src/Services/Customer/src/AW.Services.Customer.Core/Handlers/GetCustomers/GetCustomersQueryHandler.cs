using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Customer.Core.GuardClauses;
using AW.Services.Customer.Core.Handlers.GetCustomer;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Customer.Core.Handlers.GetCustomers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, GetCustomersDto?>
    {
        private readonly ILogger<GetCustomersQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.Customer> _repository;

        public GetCustomersQueryHandler(
            ILogger<GetCustomersQueryHandler> logger,
            IMapper mapper,
            IRepository<Entities.Customer> repository
        ) => (_logger, _mapper, _repository) = (logger, mapper, repository);

        public async Task<GetCustomersDto?> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");
            _logger.LogInformation("Getting customers from database");

            var spec = new GetCustomersPaginatedSpecification(
                request.PageIndex,
                request.PageSize,
                _mapper.Map<Entities.CustomerType?>(request.CustomerType),
                request.Territory,
                request.AccountNumber
            );
            var countSpec = new CountCustomersSpecification(
                _mapper.Map<Entities.CustomerType?>(request.CustomerType),
                request.Territory,
                request.AccountNumber
            );

            var customers = await _repository.ListAsync(spec, cancellationToken);
            Guard.Against.CustomersNullOrEmpty(customers, _logger);

            _logger.LogInformation("Returning customers");
            return new GetCustomersDto(
                _mapper.Map<List<CustomerDto>>(customers),
                await _repository.CountAsync(countSpec, cancellationToken)
            );
        }
    }
}
