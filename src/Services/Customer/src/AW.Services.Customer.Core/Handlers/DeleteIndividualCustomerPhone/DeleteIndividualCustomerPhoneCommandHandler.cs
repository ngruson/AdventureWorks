using Ardalis.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.SharedKernel.Extensions;
using AW.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Core.Handlers.DeleteIndividualCustomerPhone
{
    public class DeleteIndividualCustomerPhoneCommandHandler : IRequestHandler<DeleteIndividualCustomerPhoneCommand, Unit>
    {
        private readonly ILogger<DeleteIndividualCustomerPhoneCommandHandler> logger;
        private readonly IRepository<Entities.IndividualCustomer> individualCustomerRepository;

        public DeleteIndividualCustomerPhoneCommandHandler(
            ILogger<DeleteIndividualCustomerPhoneCommandHandler> logger,
            IRepository<Entities.IndividualCustomer> individualCustomerRepository
        ) => (this.logger, this.individualCustomerRepository) = (logger, individualCustomerRepository);

        public async Task<Unit> Handle(DeleteIndividualCustomerPhoneCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");
            logger.LogInformation("Getting customer from database");

            var individualCustomer = await individualCustomerRepository.GetBySpecAsync(
                new GetIndividualCustomerSpecification(request.AccountNumber),
                cancellationToken
            );
            Guard.Against.Null(individualCustomer, nameof(individualCustomer), logger);

            logger.LogInformation("Removing phone from customer");
            var phone = individualCustomer.Person.PhoneNumbers.FirstOrDefault(
                e => e.PhoneNumberType == request.Phone.PhoneNumberType &&
                    e.PhoneNumber == request.Phone.PhoneNumber
            );
            Guard.Against.Null(phone, nameof(phone), logger);

            individualCustomer.Person.RemovePhoneNumber(phone);

            logger.LogInformation("Updating customer to database");
            await individualCustomerRepository.UpdateAsync(individualCustomer, cancellationToken);
            return Unit.Value;
        }
    }
}