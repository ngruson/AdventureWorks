using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Customer.Core.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Core.Handlers.GetCustomer
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerDto>
    {
        private readonly ILogger<GetCustomerQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.Customer> _repository;

        public GetCustomerQueryHandler(
            ILogger<GetCustomerQueryHandler> logger,
            IMapper mapper,
            IRepository<Entities.Customer> repository
        ) => (_logger, _mapper, _repository) = (logger, mapper, repository);

        public async Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");
            _logger.LogInformation("Getting customer from database");

            var spec = new GetCustomerSpecification(
                request.AccountNumber
            );

            var customer = await _repository.SingleOrDefaultAsync(spec, cancellationToken);
            Guard.Against.CustomerNull(customer, request.AccountNumber, _logger);

            _logger.LogInformation("Returning customer");
            return _mapper.Map<CustomerDto>(customer);
        }
    }
}