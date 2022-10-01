using Ardalis.GuardClauses;
using AW.Services.Customer.Core.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Core.Handlers.DeleteIndividualCustomerEmailAddress
{
    public class DeleteIndividualCustomerEmailAddressCommandHandler : IRequestHandler<DeleteIndividualCustomerEmailAddressCommand, Unit>
    {
        private readonly ILogger<DeleteIndividualCustomerEmailAddressCommandHandler> _logger;
        private readonly IRepository<Entities.IndividualCustomer> _repository;

        public DeleteIndividualCustomerEmailAddressCommandHandler(
            ILogger<DeleteIndividualCustomerEmailAddressCommandHandler> logger,
            IRepository<Entities.IndividualCustomer> repository
        ) => (_logger, _repository) = (logger, repository);

        public async Task<Unit> Handle(DeleteIndividualCustomerEmailAddressCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");
            _logger.LogInformation("Getting customer from database");

            var individualCustomer = await _repository.SingleOrDefaultAsync(
                new GetIndividualCustomerSpecification(request.AccountNumber),
                cancellationToken
            );
            Guard.Against.CustomerNull(individualCustomer, request.AccountNumber, _logger);

            _logger.LogInformation("Removing address from customer");
            var emailAddress = individualCustomer.Person.EmailAddresses.FirstOrDefault(
                e => e.EmailAddress == request.EmailAddress
            );
            Guard.Against.EmailAddressNull(
                emailAddress,
                request.AccountNumber,
                request.EmailAddress.Value,
                _logger
            );

            individualCustomer.Person.RemoveEmailAddress(emailAddress);

            _logger.LogInformation("Updating customer to database");
            await _repository.UpdateAsync(individualCustomer, cancellationToken);
            return Unit.Value;
        }
    }
}