using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Customer>
    {
        private readonly ILogger<UpdateCustomerCommandHandler> _logger;
        private readonly ICustomerApiClient _client;

        public UpdateCustomerCommandHandler(ILogger<UpdateCustomerCommandHandler> logger, ICustomerApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(request.AccountNumber, _logger);
            Guard.Against.Null(request.Customer, _logger);

            _logger.LogInformation("Updating customer {AccountNumber}", request.AccountNumber);
            var customer = await _client.UpdateCustomerAsync(
                request.AccountNumber,
                request.Customer!
            );
            Guard.Against.Null(customer, _logger);

            _logger.LogInformation("Returning updated customer {AccountNumber}", request.AccountNumber);

            return customer!;
        }
    }
}
