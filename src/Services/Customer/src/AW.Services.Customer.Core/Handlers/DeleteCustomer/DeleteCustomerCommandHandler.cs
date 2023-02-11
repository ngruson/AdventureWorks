using Ardalis.GuardClauses;
using AW.Services.Customer.Core.GuardClauses;
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
        private readonly ILogger<DeleteCustomerCommandHandler> _logger;
        private readonly IRepository<Entities.Customer> _repository;

        public DeleteCustomerCommandHandler(
            ILogger<DeleteCustomerCommandHandler> logger,
            IRepository<Entities.Customer> repository
        ) => (_logger, _repository) = (logger, repository);

        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");

            _logger.LogInformation("Getting customer from database");
            var spec = new GetCustomerSpecification(request.AccountNumber);
            var customer = await _repository.SingleOrDefaultAsync(spec, cancellationToken);
            Guard.Against.CustomerNull(customer, request.AccountNumber, _logger);

            _logger.LogInformation("Deleting customer from database");
            await _repository.DeleteAsync(customer!, cancellationToken);

            return Unit.Value;
        }
    }
}