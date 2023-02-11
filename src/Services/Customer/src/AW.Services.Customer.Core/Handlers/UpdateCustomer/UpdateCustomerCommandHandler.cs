using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Customer.Core.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Customer.Core.Handlers.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerDto>
    {
        private readonly ILogger<UpdateCustomerCommandHandler> _logger;
        private readonly IRepository<Entities.Customer> _repository;
        private readonly IMapper _mapper;

        public UpdateCustomerCommandHandler(
            ILogger<UpdateCustomerCommandHandler> logger,
            IRepository<Entities.Customer> repository,
            IMapper mapper) =>
                (_logger, _repository, _mapper) = (logger, repository, mapper);

        public async Task<CustomerDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");

            _logger.LogInformation("Getting customer from database");
            var spec = new GetCustomerSpecification(request.Customer!.AccountNumber!);
            var customer = await _repository.SingleOrDefaultAsync(spec, cancellationToken);
            Guard.Against.CustomerNull(customer, request.Customer.AccountNumber!, _logger);

            _logger.LogInformation("Updating customer");
            _mapper.Map(request.Customer, customer);

            _logger.LogInformation("Saving customer to database");
            await _repository.UpdateAsync(customer!, cancellationToken);

            _logger.LogInformation("Returning customer");
            return _mapper.Map<CustomerDto>(customer);
        }
    }
}