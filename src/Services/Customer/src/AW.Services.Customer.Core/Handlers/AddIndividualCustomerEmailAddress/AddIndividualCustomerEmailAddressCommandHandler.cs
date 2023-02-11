using Ardalis.GuardClauses;
using AW.Services.Customer.Core.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Customer.Core.Handlers.AddIndividualCustomerEmailAddress
{
    public class AddIndividualCustomerEmailAddressCommandHandler : IRequestHandler<AddIndividualCustomerEmailAddressCommand, Unit>
    {
        private readonly ILogger<AddIndividualCustomerEmailAddressCommandHandler> _logger;
        private readonly IRepository<Entities.IndividualCustomer> _repository;

        public AddIndividualCustomerEmailAddressCommandHandler(
            ILogger<AddIndividualCustomerEmailAddressCommandHandler> logger,
            IRepository<Entities.IndividualCustomer> repository
        ) => (_logger, _repository) = (logger, repository);

        public async Task<Unit> Handle(AddIndividualCustomerEmailAddressCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");
            _logger.LogInformation("Getting customer from database");

            var individualCustomer = await _repository.SingleOrDefaultAsync(
                new GetIndividualCustomerSpecification(request.AccountNumber),
                cancellationToken
            );
            Guard.Against.CustomerNull(individualCustomer, request.AccountNumber, _logger);

            _logger.LogInformation("Adding email address to customer");
            var emailAddress = new Entities.PersonEmailAddress(request.EmailAddress);
            individualCustomer!.Person?.AddEmailAddress(emailAddress);

            _logger.LogInformation("Saving customer to database");
            await _repository.UpdateAsync(individualCustomer, cancellationToken);

            return Unit.Value;
        }
    }
}