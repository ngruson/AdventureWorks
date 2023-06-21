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

namespace AW.Services.Customer.Core.Handlers.UpdateStoreCustomerContact;

public class UpdateStoreCustomerContactCommandHandler : IRequestHandler<UpdateStoreCustomerContactCommand, Result>
{
    private readonly ILogger<UpdateStoreCustomerContactCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IRepository<Entities.StoreCustomer> _repository;
    private readonly IValidator<UpdateStoreCustomerContactCommand> _validator;

    public UpdateStoreCustomerContactCommandHandler(
        ILogger<UpdateStoreCustomerContactCommandHandler> logger,
        IMapper mapper,
        IRepository<Entities.StoreCustomer> repository,
        IValidator<UpdateStoreCustomerContactCommand> validator
    ) => (_logger, _mapper, _repository, _validator) = (logger, mapper, repository, validator);
    
    public async Task<Result> Handle(UpdateStoreCustomerContactCommand request, CancellationToken cancellationToken)
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

            var storeCustomer = await _repository.SingleOrDefaultAsync(
                new GetStoreCustomerSpecification(request.CustomerId),
                cancellationToken
            );
            var result = Guard.Against.CustomerNull(storeCustomer, request.CustomerId, _logger);
            if (!result.IsSuccess)
                return result;

            var contact = storeCustomer?.Contacts.Find(_ =>
                _.ObjectId == request.CustomerContact.ObjectId
            );
            result = Guard.Against.StoreContactNull(
                contact,
                request.CustomerContact.ObjectId,
                _logger
            );
            if (!result.IsSuccess)
                return result;

            _logger.LogInformation("Updating contact");
            _mapper.Map(request.CustomerContact, contact);

            _logger.LogInformation("Saving customer to database");
            await _repository.UpdateAsync(storeCustomer!, cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
            return Result.Error(ex.Message);
        }
    }
}
