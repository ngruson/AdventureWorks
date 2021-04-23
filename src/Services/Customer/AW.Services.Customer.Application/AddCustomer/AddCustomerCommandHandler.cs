using Ardalis.Specification;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Application.AddCustomer
{
    public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, CustomerDto>
    {
        private readonly ILogger<AddCustomerCommandHandler> logger;
        private readonly IRepositoryBase<Domain.Customer> customerRepository;
        private readonly IMapper mapper;

        public AddCustomerCommandHandler(
            ILogger<AddCustomerCommandHandler> logger,
            IRepositoryBase<Domain.Customer> customerRepository,
            IMapper mapper) =>
                (this.logger, this.customerRepository, this.mapper) = (logger, customerRepository, mapper);

        public async Task<CustomerDto> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");

            logger.LogInformation("Mapping request to customer");
            var customer = mapper.Map<Domain.Customer>(request.Customer);

            logger.LogInformation("Saving customer to database");
            await customerRepository.AddAsync(customer);

            logger.LogInformation("Returning customer");
            return mapper.Map<CustomerDto>(customer);
        }
    }
}