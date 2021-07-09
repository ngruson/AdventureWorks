using Ardalis.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Core.Handlers.AddIndividualCustomerEmailAddress
{
    public class AddIndividualCustomerEmailAddressCommandHandler : IRequestHandler<AddIndividualCustomerEmailAddressCommand, Unit>
    {
        private readonly ILogger<AddIndividualCustomerEmailAddressCommandHandler> logger;
        private readonly IRepository<Entities.IndividualCustomer> individualCustomerRepository;

        public AddIndividualCustomerEmailAddressCommandHandler(
            ILogger<AddIndividualCustomerEmailAddressCommandHandler> logger,
            IRepository<Entities.IndividualCustomer> individualCustomerRepository
        ) => (this.logger, this.individualCustomerRepository) = (logger, individualCustomerRepository);

        public async Task<Unit> Handle(AddIndividualCustomerEmailAddressCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");
            logger.LogInformation("Getting customer from database");

            var individualCustomer = await individualCustomerRepository.GetBySpecAsync(
                new GetIndividualCustomerSpecification(request.AccountNumber)
            );
            Guard.Against.Null(individualCustomer, nameof(individualCustomer));

            logger.LogInformation("Adding email address to customer");
            var emailAddress = new Entities.PersonEmailAddress { EmailAddress = request.EmailAddress };
            individualCustomer.Person.EmailAddresses.Add(emailAddress);

            logger.LogInformation("Saving customer to database");
            await individualCustomerRepository.UpdateAsync(individualCustomer);

            return Unit.Value;
        }
    }
}