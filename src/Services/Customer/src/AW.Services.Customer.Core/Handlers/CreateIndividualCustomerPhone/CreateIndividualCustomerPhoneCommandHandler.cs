using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using AW.Services.Customer.Core.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Customer.Core.Handlers.CreateIndividualCustomerPhone
{
    public class CreateIndividualCustomerPhoneCommandHandler : IRequestHandler<CreateIndividualCustomerPhoneCommand, Result>
    {
        private readonly ILogger<CreateIndividualCustomerPhoneCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.IndividualCustomer> _repository;
        private readonly IValidator<CreateIndividualCustomerPhoneCommand> _validator;

        public CreateIndividualCustomerPhoneCommandHandler(
            ILogger<CreateIndividualCustomerPhoneCommandHandler> logger,
            IMapper mapper,
            IRepository<Entities.IndividualCustomer> repository,
            IValidator<CreateIndividualCustomerPhoneCommand> validator
        ) => (_logger, _mapper, _repository, _validator) = (logger, mapper, repository, validator);

        public async Task<Result> Handle(CreateIndividualCustomerPhoneCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Validating command");

                var validation = await _validator.ValidateAsync(request, cancellationToken);
                if (!validation.IsValid)
                {
                    return Result.Invalid(validation.AsErrors());
                }

                _logger.LogInformation("Getting customer from database");

                var individualCustomer = await _repository.SingleOrDefaultAsync(
                    new GetIndividualCustomerSpecification(request.CustomerId),
                    cancellationToken
                );
                var result = Guard.Against.CustomerNull(individualCustomer, request.CustomerId, _logger);
                if (!result.IsSuccess)
                    return result;

                _logger.LogInformation("Adding phone to customer");
                var phone = _mapper.Map<Entities.PersonPhone>(request.Phone);
                individualCustomer!.Person?.AddPhoneNumber(phone);

                _logger.LogInformation("Saving customer to database");
                await _repository.UpdateAsync(individualCustomer, cancellationToken);

                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                return Result.Error(ex.Message);
            }
        }
    }
}
