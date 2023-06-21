using Ardalis.GuardClauses;
using Ardalis.Result;
using AutoMapper;
using AW.Services.Customer.Core.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Customer.Core.Handlers.GetCustomers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, Result<List<Customer>>>
    {
        private readonly ILogger<GetCustomersQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.Customer> _repository;

        public GetCustomersQueryHandler(
            ILogger<GetCustomersQueryHandler> logger,
            IMapper mapper,
            IRepository<Entities.Customer> repository
        ) => (_logger, _mapper, _repository) = (logger, mapper, repository);

        public async Task<Result<List<Customer>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Getting customers from database");

                var spec = new GetCustomersSpecification(
                    _mapper.Map<Entities.CustomerType?>(request.CustomerType),
                    request.IncludeDetails
                );

                var customers = await _repository.ListAsync(spec, cancellationToken);
                var result = Guard.Against.CustomersNullOrEmpty(customers, _logger);
                if (!result.IsSuccess)
                    return result;

                _logger.LogInformation("Returning {Count} customers", customers.Count);
                return _mapper.Map<List<Customer>>(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                return Result.Error(ex.Message);
            }
        }
    }
}
