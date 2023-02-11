using Ardalis.GuardClauses;
using AW.Services.Customer.Core.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Customer.Core.Handlers.DeleteIndividualCustomerPhone
{
    public class DeleteIndividualCustomerPhoneCommandHandler : IRequestHandler<DeleteIndividualCustomerPhoneCommand, Unit>
    {
        private readonly ILogger<DeleteIndividualCustomerPhoneCommandHandler> _logger;
        private readonly IRepository<Entities.IndividualCustomer> _repository;

        public DeleteIndividualCustomerPhoneCommandHandler(
            ILogger<DeleteIndividualCustomerPhoneCommandHandler> logger,
            IRepository<Entities.IndividualCustomer> repository
        ) => (_logger, _repository) = (logger, repository);

        public async Task<Unit> Handle(DeleteIndividualCustomerPhoneCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");
            _logger.LogInformation("Getting customer from database");

            var individualCustomer = await _repository.SingleOrDefaultAsync(
                new GetIndividualCustomerSpecification(request.AccountNumber),
                cancellationToken
            );
            Guard.Against.CustomerNull(individualCustomer, request.AccountNumber, _logger);

            _logger.LogInformation("Removing phone from customer");
            var phone = individualCustomer!.Person!.PhoneNumbers.FirstOrDefault(
                e => e.PhoneNumberType == request.Phone.PhoneNumberType &&
                    e.PhoneNumber == request.Phone.PhoneNumber
            );
            Guard.Against.PhoneNumberNull(
                phone, 
                request.AccountNumber, 
                request.Phone.PhoneNumber!,
                request.Phone.PhoneNumberType!,
                _logger
            );

            individualCustomer.Person.RemovePhoneNumber(phone!);

            _logger.LogInformation("Updating customer to database");
            await _repository.UpdateAsync(individualCustomer, cancellationToken);
            return Unit.Value;
        }
    }
}