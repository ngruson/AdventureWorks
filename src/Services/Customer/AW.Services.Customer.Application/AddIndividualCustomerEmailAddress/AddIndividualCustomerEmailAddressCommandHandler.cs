using Ardalis.GuardClauses;
using Ardalis.Specification;
using AutoMapper;
using AW.Services.Customer.Application.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Application.AddIndividualCustomerEmailAddress
{
    public class AddIndividualCustomerEmailAddressCommandHandler : IRequestHandler<AddIndividualCustomerEmailAddressCommand, Unit>
    {
        private readonly ILogger<AddIndividualCustomerEmailAddressCommandHandler> logger;
        private readonly IMapper mapper;
        private readonly IRepositoryBase<Domain.IndividualCustomer> individualCustomerRepository;

        public AddIndividualCustomerEmailAddressCommandHandler(
            ILogger<AddIndividualCustomerEmailAddressCommandHandler> logger,
            IMapper mapper,
            IRepositoryBase<Domain.IndividualCustomer> individualCustomerRepository
        ) => (this.logger, this.mapper, this.individualCustomerRepository) = (logger, mapper, individualCustomerRepository);

        public async Task<Unit> Handle(AddIndividualCustomerEmailAddressCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");
            logger.LogInformation("Getting customer from database");

            var individualCustomer = await individualCustomerRepository.GetBySpecAsync(
                new GetIndividualCustomerSpecification(request.AccountNumber)
            );
            Guard.Against.Null(individualCustomer, nameof(individualCustomer));

            logger.LogInformation("Adding email address to customer");
            var emailAddress = new Domain.PersonEmailAddress { EmailAddress = request.EmailAddress };
            individualCustomer.Person.EmailAddresses.Add(emailAddress);

            logger.LogInformation("Saving customer to database");
            await individualCustomerRepository.UpdateAsync(individualCustomer);

            return Unit.Value;
        }
    }
}