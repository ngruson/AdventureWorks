using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AW.Services.Customer.Core.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Customer.Core.Handlers.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Result>
    {
        private readonly ILogger<DeleteCustomerCommandHandler> _logger;
        private readonly IRepository<Entities.Customer> _repository;
        private readonly IValidator<DeleteCustomerCommand> _validator;

        public DeleteCustomerCommandHandler(
            ILogger<DeleteCustomerCommandHandler> logger,
            IRepository<Entities.Customer> repository,
            IValidator<DeleteCustomerCommand> validator
        ) => (_logger, _repository, _validator) = (logger, repository, validator);

        public async Task<Result> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
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
                var spec = new GetCustomerSpecification(request.ObjectId);
                var customer = await _repository.SingleOrDefaultAsync(spec, cancellationToken);
                var result = Guard.Against.CustomerNull(customer, request.ObjectId, _logger);
                if (!result.IsSuccess)
                    return result;

                _logger.LogInformation("Deleting customer from database");
                await _repository.DeleteAsync(customer!, cancellationToken);

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
