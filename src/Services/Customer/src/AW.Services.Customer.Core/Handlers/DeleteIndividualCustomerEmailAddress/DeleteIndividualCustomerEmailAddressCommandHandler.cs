using Ardalis.GuardClauses;
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
        private readonly ILogger<DeleteIndividualCustomerEmailAddressCommandHandler> logger;
        private readonly IRepository<Entities.IndividualCustomer> individualCustomerRepository;

        public DeleteIndividualCustomerEmailAddressCommandHandler(
            ILogger<DeleteIndividualCustomerEmailAddressCommandHandler> logger,
            IRepository<Entities.IndividualCustomer> individualCustomerRepository
        ) => (this.logger, this.individualCustomerRepository) = (logger, individualCustomerRepository);

        public async Task<Unit> Handle(DeleteIndividualCustomerEmailAddressCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");
            logger.LogInformation("Getting customer from database");

            var individualCustomer = await individualCustomerRepository.GetBySpecAsync(
                new GetIndividualCustomerSpecification(request.AccountNumber),
                cancellationToken
            );
            Guard.Against.Null(individualCustomer, nameof(individualCustomer), logger);

            logger.LogInformation("Removing address from customer");
            var emailAddress = individualCustomer.Person.EmailAddresses.FirstOrDefault(
                e => e.EmailAddress == request.EmailAddress
            );
            Guard.Against.Null(emailAddress, nameof(emailAddress), logger);

            individualCustomer.Person.RemoveEmailAddress(emailAddress);

            logger.LogInformation("Updating customer to database");
            await individualCustomerRepository.UpdateAsync(individualCustomer, cancellationToken);
            return Unit.Value;
        }
    }
}