using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AW.Services.Customer.Core.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Customer.Core.Handlers.CreateIndividualCustomerEmailAddress
{
    public class CreateIndividualCustomerEmailAddressCommandHandler : IRequestHandler<CreateIndividualCustomerEmailAddressCommand, Result>
    {
        private readonly ILogger<CreateIndividualCustomerEmailAddressCommandHandler> _logger;
        private readonly IRepository<Entities.IndividualCustomer> _repository;
        private readonly IValidator<CreateIndividualCustomerEmailAddressCommand> _validator;

        public CreateIndividualCustomerEmailAddressCommandHandler(
            ILogger<CreateIndividualCustomerEmailAddressCommandHandler> logger,
            IRepository<Entities.IndividualCustomer> repository,
            IValidator<CreateIndividualCustomerEmailAddressCommand> validator
        ) => (_logger, _repository, _validator) = (logger, repository, validator);

        public async Task<Result> Handle(CreateIndividualCustomerEmailAddressCommand request, CancellationToken cancellationToken)
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

                _logger.LogInformation("Adding email address to customer");
                var emailAddress = new Entities.PersonEmailAddress(request.EmailAddress);
                individualCustomer!.Person?.AddEmailAddress(emailAddress);

                _logger.LogInformation("Saving customer to database");
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
