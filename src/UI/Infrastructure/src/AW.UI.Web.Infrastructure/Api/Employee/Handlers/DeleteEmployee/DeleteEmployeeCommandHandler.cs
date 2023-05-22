using AW.UI.Web.Infrastructure.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.Employee.Handlers.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly ILogger<DeleteEmployeeCommandHandler> _logger;
        private readonly IEmployeeApiClient _client;

        public DeleteEmployeeCommandHandler(
            ILogger<DeleteEmployeeCommandHandler> logger,
            IEmployeeApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting employee");
            await _client.DeleteEmployee(request);
            _logger.LogInformation("Employee succesfully deleted");
        }
    }
}
