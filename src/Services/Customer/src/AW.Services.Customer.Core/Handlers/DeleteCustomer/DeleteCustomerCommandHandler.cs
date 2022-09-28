using Ardalis.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Core.Handlers.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Unit>
    {
        private readonly ILogger<DeleteCustomerCommandHandler> logger;
        private readonly IRepository<Entities.Customer> customerRepository;

        public DeleteCustomerCommandHandler(
            ILogger<DeleteCustomerCommandHandler> logger,
            IRepository<Entities.Customer> customerRepository
        ) => (this.logger, this.customerRepository) = (logger, customerRepository);

        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");

            logger.LogInformation("Getting customer from database");
            var spec = new GetCustomerSpecification(request.AccountNumber);
            var customer = await customerRepository.SingleOrDefaultAsync(spec, cancellationToken);
            Guard.Against.Null(customer, logger);

            logger.LogInformation("Deleting customer from database");
            await customerRepository.DeleteAsync(customer, cancellationToken);

            return Unit.Value;
        }
    }
}