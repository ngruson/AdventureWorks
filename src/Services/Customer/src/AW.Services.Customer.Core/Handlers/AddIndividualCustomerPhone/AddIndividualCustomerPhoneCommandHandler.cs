using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Core.Handlers.AddIndividualCustomerPhone
{
    public class AddIndividualCustomerPhoneCommandHandler : IRequestHandler<AddIndividualCustomerPhoneCommand, Unit>
    {
        private readonly ILogger<AddIndividualCustomerPhoneCommandHandler> logger;
        private readonly IMapper mapper;
        private readonly IRepository<Entities.IndividualCustomer> individualCustomerRepository;

        public AddIndividualCustomerPhoneCommandHandler(
            ILogger<AddIndividualCustomerPhoneCommandHandler> logger,
            IMapper mapper,
            IRepository<Entities.IndividualCustomer> individualCustomerRepository
        ) => (this.logger, this.mapper, this.individualCustomerRepository) = (logger, mapper, individualCustomerRepository);

        public async Task<Unit> Handle(AddIndividualCustomerPhoneCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");
            logger.LogInformation("Getting customer from database");

            var individualCustomer = await individualCustomerRepository.SingleOrDefaultAsync(
                new GetIndividualCustomerSpecification(request.AccountNumber),
                cancellationToken
            );
            Guard.Against.Null(individualCustomer, logger);

            logger.LogInformation("Adding phone to customer");
            var phone = mapper.Map<Entities.PersonPhone>(request.Phone);
            individualCustomer.Person.AddPhoneNumber(phone);

            logger.LogInformation("Saving customer to database");
            await individualCustomerRepository.UpdateAsync(individualCustomer, cancellationToken);

            return Unit.Value;
        }
    }
}