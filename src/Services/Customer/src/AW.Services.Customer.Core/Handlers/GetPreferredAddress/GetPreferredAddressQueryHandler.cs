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

namespace AW.Services.Customer.Core.Handlers.GetPreferredAddress
{
    public class GetPreferredAddressQueryHandler : IRequestHandler<GetPreferredAddressQuery, Result<AddressDto?>>
    {
        private readonly ILogger<GetPreferredAddressQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.Customer> _repository;
        private readonly IValidator<GetPreferredAddressQuery> _validator;

        public GetPreferredAddressQueryHandler(
            ILogger<GetPreferredAddressQueryHandler> logger,
            IMapper mapper,
            IRepository<Entities.Customer> repository,
            IValidator<GetPreferredAddressQuery> validator
        ) => 
            (_logger, _mapper, _repository, _validator) = (logger, mapper, repository, validator);

        public async Task<Result<AddressDto?>> Handle(GetPreferredAddressQuery request, CancellationToken cancellationToken)
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

                var customer = await _repository.SingleOrDefaultAsync(
                    new GetCustomerWithAddressesSpecification(request.CustomerId),
                    cancellationToken
                );
                var result = Guard.Against.CustomerNull(customer, request.CustomerId, _logger);
                if (!result.IsSuccess)
                    return result;

                var address = customer!.GetPreferredAddress(request.AddressType!);
                if (address == null)
                    return Result.NotFound($"{request.AddressType} address not found");

                _logger.LogInformation("Returning address");
                return _mapper.Map<AddressDto>(address);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                return Result.Error(ex.Message);
            }
        }
    }
}
