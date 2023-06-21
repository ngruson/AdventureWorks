using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AW.Services.Customer.Core.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Customer.Core.Handlers.DeleteIndividualCustomerEmailAddress
{
    public class DeleteIndividualCustomerEmailAddressCommandHandler : IRequestHandler<DeleteIndividualCustomerEmailAddressCommand, Result>
    {
        private readonly ILogger<DeleteIndividualCustomerEmailAddressCommandHandler> _logger;
        private readonly IRepository<Entities.IndividualCustomer> _repository;
        private readonly IValidator<DeleteIndividualCustomerEmailAddressCommand> _validator;

        public DeleteIndividualCustomerEmailAddressCommandHandler(
            ILogger<DeleteIndividualCustomerEmailAddressCommandHandler> logger,
            IRepository<Entities.IndividualCustomer> repository,
            IValidator<DeleteIndividualCustomerEmailAddressCommand> validator
        ) => (_logger, _repository, _validator) = (logger, repository, validator);

        public async Task<Result> Handle(DeleteIndividualCustomerEmailAddressCommand request, CancellationToken cancellationToken)
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

                _logger.LogInformation("Removing address from customer");
                var emailAddress = individualCustomer!.Person!.EmailAddresses.Find(
                    e => e.ObjectId == request.EmailAddressId
                );
                result = Guard.Against.EmailAddressNull(
                    emailAddress,
                    request.EmailAddressId,
                    _logger
                );
                if (!result.IsSuccess)
                    return result;

                individualCustomer.Person.RemoveEmailAddress(emailAddress!);

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
