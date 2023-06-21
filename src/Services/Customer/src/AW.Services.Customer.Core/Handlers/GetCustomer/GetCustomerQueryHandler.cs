using Ardalis.GuardClauses;
using Ardalis.Result;
using AutoMapper;
using AW.Services.Customer.Core.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Customer.Core.Handlers.GetCustomer
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Result<Customer>>
    {
        private readonly ILogger<GetCustomerQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.Customer> _repository;

        public GetCustomerQueryHandler(
            ILogger<GetCustomerQueryHandler> logger,
            IMapper mapper,
            IRepository<Entities.Customer> repository
        ) => (_logger, _mapper, _repository) = (logger, mapper, repository);

        public async Task<Result<Customer>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Getting customer from database");

                var spec = new GetCustomerSpecification(
                    request.ObjectId
                );

                var customer = await _repository.SingleOrDefaultAsync(spec, cancellationToken);
                var result = Guard.Against.CustomerNull(customer, request.ObjectId, _logger);
                if (!result.IsSuccess)
                    return result;

                _logger.LogInformation("Returning customer");
                return Result.Success(
                    _mapper.Map<Customer>(customer)
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                return Result.Error(ex.Message);
            }
        }
    }
}
