using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Customer.Core.Specifications;
using AW.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Core.Handlers.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerDto>
    {
        private readonly ILogger<UpdateCustomerCommandHandler> logger;
        private readonly IRepository<Entities.Customer> customerRepository;
        private readonly IMapper mapper;

        public UpdateCustomerCommandHandler(
            ILogger<UpdateCustomerCommandHandler> logger,
            IRepository<Entities.Customer> customerRepository,
            IMapper mapper) =>
                (this.logger, this.customerRepository, this.mapper) = (logger, customerRepository, mapper);

        public async Task<CustomerDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");

            logger.LogInformation("Getting customer from database");
            var spec = new GetCustomerSpecification(request.Customer.AccountNumber);
            var customer = await customerRepository.GetBySpecAsync(spec);
            Guard.Against.Null(customer, nameof(customer));

            logger.LogInformation("Updating customer");
            mapper.Map(request.Customer, customer);

            logger.LogInformation("Saving customer to database");
            await customerRepository.UpdateAsync(customer);

            logger.LogInformation("Returning customer");
            return mapper.Map<CustomerDto>(customer);
        }
    }
}