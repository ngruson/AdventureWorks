using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Core.Handlers.AddCustomerAddress
{
    public class AddCustomerAddressCommandHandler : IRequestHandler<AddCustomerAddressCommand, Unit>
    {
        private readonly ILogger<AddCustomerAddressCommandHandler> logger;
        private readonly IMapper mapper;
        private readonly IRepository<Entities.Customer> customerRepository;

        public AddCustomerAddressCommandHandler(
            ILogger<AddCustomerAddressCommandHandler> logger,
            IMapper mapper,
            IRepository<Entities.Customer> customerRepository
        ) => (this.logger, this.mapper, this.customerRepository) = (logger, mapper, customerRepository);

        public async Task<Unit> Handle(AddCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");
            logger.LogInformation("Getting customer from database");

            var customer = await customerRepository.SingleOrDefaultAsync(
                new GetCustomerSpecification(request.AccountNumber),
                cancellationToken
            );
            Guard.Against.Null(customer, nameof(customer));

            logger.LogInformation("Adding address to customer");
            var customerAddress = mapper.Map<Entities.CustomerAddress>(request.CustomerAddress);
            customer.AddAddress(customerAddress);

            logger.LogInformation("Saving customer to database");
            await customerRepository.UpdateAsync(customer, cancellationToken);

            return Unit.Value;
        }
    }
}