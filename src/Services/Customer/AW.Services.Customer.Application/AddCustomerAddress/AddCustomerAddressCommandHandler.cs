using Ardalis.Specification;
using AutoMapper;
using AW.Services.Customer.Application.Specifications;
using AW.Services.Customer.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Application.AddCustomerAddress
{
    public class AddCustomerAddressCommandHandler : IRequestHandler<AddCustomerAddressCommand, Unit>
    {
        private readonly ILogger<AddCustomerAddressCommandHandler> logger;
        private readonly IMapper mapper;
        private readonly IRepositoryBase<Domain.Customer> customerRepository;

        public AddCustomerAddressCommandHandler(
            ILogger<AddCustomerAddressCommandHandler> logger,
            IMapper mapper,
            IRepositoryBase<Domain.Customer> customerRepository
        ) => (this.logger, this.mapper, this.customerRepository) = (logger, mapper, customerRepository);

        public async Task<Unit> Handle(AddCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");
            logger.LogInformation("Getting customer from database");

            var customer = await customerRepository.GetBySpecAsync(
                new GetCustomerSpecification(request.AccountNumber)
            );

            logger.LogInformation("Adding address to customer");
            var customerAddress = mapper.Map<CustomerAddress>(request.CustomerAddress);
            customer.Addresses.Add(customerAddress);

            logger.LogInformation("Saving customer to database");
            await customerRepository.UpdateAsync(customer);

            return Unit.Value;
        }
    }
}