using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Customer.Core.Handlers.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<CreatedCustomer>>
    {
        private readonly ILogger<CreateCustomerCommandHandler> _logger;
        private readonly IRepository<Entities.Customer> _customerRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCustomerCommand> _validator;

        public CreateCustomerCommandHandler(
            ILogger<CreateCustomerCommandHandler> logger,
            IRepository<Entities.Customer> customerRepository,
            IMapper mapper,
            IValidator<CreateCustomerCommand> validator
        ) => (_logger, _customerRepository, _mapper, _validator) = (logger, customerRepository, mapper, validator);

        public async Task<Result<CreatedCustomer>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Validating command");

                var validation = await _validator.ValidateAsync(request, cancellationToken);
                if (!validation.IsValid)
                {
                    return Result.Invalid(validation.AsErrors());
                }

                _logger.LogInformation("Mapping request to customer");
                var customer = _mapper.Map<Entities.Customer>(request.Customer);

                _logger.LogInformation("Saving customer to database");
                await _customerRepository.AddAsync(customer, cancellationToken);

                _logger.LogInformation("Returning customer");
                return _mapper.Map<CreatedCustomer>(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                return Result.Error(ex.Message);
            }
        }
    }
}
