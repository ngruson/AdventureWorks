using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AW.Services.Customer.Core.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Customer.Core.Handlers.DeleteCustomerAddress
{
    public class DeleteCustomerAddressCommandHandler : IRequestHandler<DeleteCustomerAddressCommand, Result>
    {
        private readonly ILogger<DeleteCustomerAddressCommandHandler> _logger;
        private readonly IRepository<Entities.Customer> _repository;
        private readonly IValidator<DeleteCustomerAddressCommand> _validator;

        public DeleteCustomerAddressCommandHandler(
            ILogger<DeleteCustomerAddressCommandHandler> logger,
            IRepository<Entities.Customer> repository,
            IValidator<DeleteCustomerAddressCommand> validator
        ) => (_logger, _repository, _validator) = (logger, repository, validator);
        
        public async Task<Result> Handle(DeleteCustomerAddressCommand request, CancellationToken cancellationToken)
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

                var customer = await _repository.SingleOrDefaultAsync(
                    new GetCustomerSpecification(request.CustomerId),
                    cancellationToken
                );
                var result = Guard.Against.CustomerNull(customer, request.CustomerId, _logger);
                if (!result.IsSuccess)
                    return result;

                _logger.LogInformation("Removing address from customer");
                var customerAddress = customer!.Addresses.FirstOrDefault(
                    a => a.ObjectId == request.AddressId
                );
                result = Guard.Against.AddressNull(
                    customerAddress,
                    request.AddressId,
                    _logger
                );
                if (!result.IsSuccess)
                    return result;

                customer.RemoveAddress(customerAddress!);

                _logger.LogInformation("Updating customer to database");
                await _repository.UpdateAsync(customer, cancellationToken);

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
