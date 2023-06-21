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

namespace AW.Services.Customer.Core.Handlers.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result<Customer>>
    {
        private readonly ILogger<UpdateCustomerCommandHandler> _logger;
        private readonly IRepository<Entities.Customer> _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateCustomerCommand> _validator;

        public UpdateCustomerCommandHandler(
            ILogger<UpdateCustomerCommandHandler> logger,
            IRepository<Entities.Customer> repository,
            IMapper mapper,
            IValidator<UpdateCustomerCommand> validator
        ) =>
            (_logger, _repository, _mapper, _validator) = (logger, repository, mapper, validator);

        public async Task<Result<Customer>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
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
                var spec = new GetCustomerSpecification(request.Customer!.ObjectId);
                var customer = await _repository.SingleOrDefaultAsync(spec, cancellationToken);
                var result = Guard.Against.CustomerNull(customer, request.Customer.ObjectId, _logger);
                if (!result.IsSuccess)
                    return result;

                _logger.LogInformation("Updating customer");
                _mapper.Map(request.Customer, customer);

                _logger.LogInformation("Saving customer to database");
                await _repository.UpdateAsync(customer!, cancellationToken);

                _logger.LogInformation("Returning customer");
                return _mapper.Map<Customer>(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                return Result.Error(ex.Message);
            }
        }
    }
}
