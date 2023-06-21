using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using AW.Services.Customer.Core.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Customer.Core.Handlers.CreateCustomerAddress
{
    public class CreateCustomerAddressCommandHandler : IRequestHandler<CreateCustomerAddressCommand, Result>
    {
        private readonly ILogger<CreateCustomerAddressCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.Customer> _customerRepository;
        private readonly IValidator<CreateCustomerAddressCommand> _validator;

        public CreateCustomerAddressCommandHandler(
            ILogger<CreateCustomerAddressCommandHandler> logger,
            IMapper mapper,
            IRepository<Entities.Customer> customerRepository,
            IValidator<CreateCustomerAddressCommand> validator
        ) => (_logger, _mapper, _customerRepository, _validator) =
            (logger, mapper, customerRepository, validator);

        public async Task<Result> Handle(CreateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Validating command");

                var validation = await _validator.ValidateAsync(request, cancellationToken);
                if (!validation.IsValid)
                {
                    return Result.Invalid(validation.AsErrors());
                }

                _logger.LogInformation("Getting customer from database");

                var customer = await _customerRepository.SingleOrDefaultAsync(
                    new GetCustomerSpecification(request.CustomerId),
                    cancellationToken
                );
                var result = Guard.Against.CustomerNull(customer, request.CustomerId, _logger);
                if (!result.IsSuccess)
                    return result;

                _logger.LogInformation("Adding address to customer");
                var customerAddress = _mapper.Map<Entities.CustomerAddress>(request.CustomerAddress);
                customer!.AddAddress(customerAddress);

                _logger.LogInformation("Saving customer to database");
                await _customerRepository.UpdateAsync(customer, cancellationToken);

                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                return Result.Error(ex.Message);
            }
        }
    }
}
