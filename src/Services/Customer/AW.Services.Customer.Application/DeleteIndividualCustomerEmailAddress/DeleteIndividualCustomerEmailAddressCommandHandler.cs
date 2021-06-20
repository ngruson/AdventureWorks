using Ardalis.GuardClauses;
using Ardalis.Specification;
using AW.Common.Extensions;
using AW.Services.Customer.Application.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Application.DeleteIndividualCustomerEmailAddress
{
    public class DeleteIndividualCustomerEmailAddressCommandHandler : IRequestHandler<DeleteIndividualCustomerEmailAddressCommand, Unit>
    {
        private readonly ILogger<DeleteIndividualCustomerEmailAddressCommandHandler> logger;
        private readonly IRepositoryBase<Domain.IndividualCustomer> individualCustomerRepository;

        public DeleteIndividualCustomerEmailAddressCommandHandler(
            ILogger<DeleteIndividualCustomerEmailAddressCommandHandler> logger,
            IRepositoryBase<Domain.IndividualCustomer> individualCustomerRepository
        ) => (this.logger, this.individualCustomerRepository) = (logger, individualCustomerRepository);

        public async Task<Unit> Handle(DeleteIndividualCustomerEmailAddressCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");
            logger.LogInformation("Getting customer from database");

            var individualCustomer = await individualCustomerRepository.GetBySpecAsync(
                new GetIndividualCustomerSpecification(request.AccountNumber)
            );
            Guard.Against.Null(individualCustomer, nameof(individualCustomer), logger);

            logger.LogInformation("Removing address from customer");
            var emailAddress = individualCustomer.Person.EmailAddresses.FirstOrDefault(
                e => e.EmailAddress == request.EmailAddress
            );
            Guard.Against.Null(emailAddress, nameof(emailAddress), logger);

            individualCustomer.Person.EmailAddresses.Remove(emailAddress);

            logger.LogInformation("Updating customer to database");
            await individualCustomerRepository.UpdateAsync(individualCustomer);
            return Unit.Value;
        }
    }
}