using Ardalis.GuardClauses;
using Ardalis.Specification;
using AW.Common.Extensions;
using AW.Services.Customer.Application.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Application.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Unit>
    {
        private readonly ILogger<DeleteCustomerCommandHandler> logger;
        private readonly IRepositoryBase<Domain.Customer> customerRepository;

        public DeleteCustomerCommandHandler(
            ILogger<DeleteCustomerCommandHandler> logger,
            IRepositoryBase<Domain.Customer> customerRepository
        ) => (this.logger, this.customerRepository) = (logger, customerRepository);

        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");

            logger.LogInformation("Getting customer from database");
            var spec = new GetCustomerSpecification(request.AccountNumber);
            var customer = await customerRepository.GetBySpecAsync(spec);
            Guard.Against.Null(customer, nameof(customer), logger);

            logger.LogInformation("Deleting customer from database");
            await customerRepository.DeleteAsync(customer);

            return Unit.Value;
        }
    }
}