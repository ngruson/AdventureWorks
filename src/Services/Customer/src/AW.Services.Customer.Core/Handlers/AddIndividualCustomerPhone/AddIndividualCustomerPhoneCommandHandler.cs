using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Customer.Core.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Customer.Core.Handlers.AddIndividualCustomerPhone
{
    public class AddIndividualCustomerPhoneCommandHandler : IRequestHandler<AddIndividualCustomerPhoneCommand, Unit>
    {
        private readonly ILogger<AddIndividualCustomerPhoneCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.IndividualCustomer> _repository;

        public AddIndividualCustomerPhoneCommandHandler(
            ILogger<AddIndividualCustomerPhoneCommandHandler> logger,
            IMapper mapper,
            IRepository<Entities.IndividualCustomer> repository
        ) => (_logger, _mapper, _repository) = (logger, mapper, repository);

        public async Task<Unit> Handle(AddIndividualCustomerPhoneCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");
            _logger.LogInformation("Getting customer from database");

            var individualCustomer = await _repository.SingleOrDefaultAsync(
                new GetIndividualCustomerSpecification(request.AccountNumber),
                cancellationToken
            );
            Guard.Against.CustomerNull(individualCustomer, request.AccountNumber, _logger);

            _logger.LogInformation("Adding phone to customer");
            var phone = _mapper.Map<Entities.PersonPhone>(request.Phone);
            individualCustomer!.Person?.AddPhoneNumber(phone);

            _logger.LogInformation("Saving customer to database");
            await _repository.UpdateAsync(individualCustomer, cancellationToken);

            return Unit.Value;
        }
    }
}