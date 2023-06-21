using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AW.Services.Customer.Core.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Customer.Core.Handlers.DeleteIndividualCustomerPhone
{
    public class DeleteIndividualCustomerPhoneCommandHandler : IRequestHandler<DeleteIndividualCustomerPhoneCommand, Result>
    {
        private readonly ILogger<DeleteIndividualCustomerPhoneCommandHandler> _logger;
        private readonly IRepository<Entities.IndividualCustomer> _repository;
        private readonly IValidator<DeleteIndividualCustomerPhoneCommand> _validator;

        public DeleteIndividualCustomerPhoneCommandHandler(
            ILogger<DeleteIndividualCustomerPhoneCommandHandler> logger,
            IRepository<Entities.IndividualCustomer> repository,
            IValidator<DeleteIndividualCustomerPhoneCommand> validator
        ) => (_logger, _repository, _validator) = (logger, repository, validator);

        public async Task<Result> Handle(DeleteIndividualCustomerPhoneCommand request, CancellationToken cancellationToken)
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

                var individualCustomer = await _repository.SingleOrDefaultAsync(
                    new GetIndividualCustomerSpecification(request.CustomerId),
                    cancellationToken
                );
                var result = Guard.Against.CustomerNull(individualCustomer, request.CustomerId, _logger);
                if (!result.IsSuccess)
                    return result;

                _logger.LogInformation("Removing phone from customer");
                var phone = individualCustomer!.Person!.PhoneNumbers.FirstOrDefault(
                    _ => _.ObjectId == request.PhoneId
                );
                result = Guard.Against.PhoneNumberNull(
                    phone,
                    request.PhoneId,
                    _logger
                );
                if (!result.IsSuccess)
                    return result;

                individualCustomer.Person.RemovePhoneNumber(phone!);

                _logger.LogInformation("Updating customer to database");
                await _repository.UpdateAsync(individualCustomer, cancellationToken);

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
