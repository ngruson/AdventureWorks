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

namespace AW.Services.Customer.Core.Handlers.UpdateCustomerAddress;

public class UpdateCustomerAddressCommandHandler : IRequestHandler<UpdateCustomerAddressCommand, Result>
{
    private readonly ILogger<UpdateCustomerAddressCommandHandler> _logger;
    private readonly IMapper _mapper;        
    private readonly IRepository<Entities.Customer> _customerRepository;
    private readonly IRepository<Entities.Address> _addressRepository;
    private readonly IValidator<UpdateCustomerAddressCommand> _validator;

    public UpdateCustomerAddressCommandHandler(
        ILogger<UpdateCustomerAddressCommandHandler> logger,
        IMapper mapper,            
        IRepository<Entities.Customer> customerRepository,
        IRepository<Entities.Address> addressRepository,
        IValidator<UpdateCustomerAddressCommand> validator
    ) => (_logger, _mapper, _customerRepository, _addressRepository, _validator) =
            (logger, mapper, customerRepository, addressRepository, validator);

    public async Task<Result> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
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

            var customer = await _customerRepository.SingleOrDefaultAsync(
                new GetCustomerSpecification(request.CustomerId),
                cancellationToken
            );
            var result = Guard.Against.CustomerNull(customer, request.CustomerId, _logger);
            if (!result.IsSuccess)
                return result;

            _logger.LogInformation("Getting address from database");
            var customerAddress = customer!.Addresses.Find(
                _ => _.ObjectId == request.CustomerAddress?.ObjectId
            );
            result = Guard.Against.AddressNull(
                customerAddress,
                request.CustomerAddress!.ObjectId,
                _logger
            );
            if (!result.IsSuccess)
                return result;

            var existingAddress = await IsExistingAddress(request.CustomerAddress.Address!);

            if (existingAddress != null)
            {
                _logger.LogInformation("Found existing address");
                customerAddress!.Address = existingAddress;
            }
            else
            {
                _logger.LogInformation("Add new address");
                customerAddress!.Address = _mapper.Map<Entities.Address>(request.CustomerAddress.Address);
            }

            _logger.LogInformation("Saving customer to database");
            await _customerRepository.UpdateAsync(customer, cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
            return Result.Error(ex.Message);
        }
    }

    private async Task<Entities.Address?> IsExistingAddress(Address addressDto)
    {
        var address = await _addressRepository.SingleOrDefaultAsync(
            new GetAddressSpecification(
                addressDto.AddressLine1!,
                addressDto.AddressLine2!,
                addressDto.PostalCode!,
                addressDto.City!,
                addressDto.StateProvinceCode!,
                addressDto.CountryRegionCode!
            )
        );

        if (address != null)
            return address;

        return null;
    }
}
