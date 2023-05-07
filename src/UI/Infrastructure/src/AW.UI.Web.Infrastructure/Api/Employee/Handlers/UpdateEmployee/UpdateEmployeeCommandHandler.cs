using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.Employee.Handlers.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly ILogger<UpdateEmployeeCommandHandler> _logger;
        private readonly IEmployeeApiClient _client;

        public UpdateEmployeeCommandHandler(
            ILogger<UpdateEmployeeCommandHandler> logger,
            IEmployeeApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request.Employee, _logger);

            _logger.LogInformation("Updating employee");
            await _client.UpdateEmployee(request.Key, request.Employee);
            _logger.LogInformation("Employee succesfully updated");
        }
    }
}
