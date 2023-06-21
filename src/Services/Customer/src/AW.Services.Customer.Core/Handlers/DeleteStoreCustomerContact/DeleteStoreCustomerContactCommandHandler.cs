using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AW.Services.Customer.Core.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Customer.Core.Handlers.DeleteStoreCustomerContact
{
    public class DeleteStoreCustomerContactCommandHandler : IRequestHandler<DeleteStoreCustomerContactCommand, Result>
    {
        private readonly ILogger<DeleteStoreCustomerContactCommandHandler> _logger;
        private readonly IRepository<Entities.StoreCustomer> _repository;
        private readonly IValidator<DeleteStoreCustomerContactCommand> _validator;

        public DeleteStoreCustomerContactCommandHandler(
            ILogger<DeleteStoreCustomerContactCommandHandler> logger,
            IRepository<Entities.StoreCustomer> storeCustomerRepository,
            IValidator<DeleteStoreCustomerContactCommand> validator
        ) => (_logger, _repository, _validator) = (logger, storeCustomerRepository, validator);
        
        public async Task<Result> Handle(DeleteStoreCustomerContactCommand request, CancellationToken cancellationToken)
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

                _logger.LogInformation("Removing phone from contact");
                var contact = storeCustomer!.Contacts.Find(
                    c => c.ObjectId == request.ContactId
                );
                result = Guard.Against.StoreContactNull(contact,
                    request.ContactId,
                    _logger
                );
                if (!result.IsSuccess)
                    return result;

                storeCustomer.RemoveContact(contact!);

                _logger.LogInformation("Updating customer to database");
                await _repository.UpdateAsync(storeCustomer, cancellationToken);

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
