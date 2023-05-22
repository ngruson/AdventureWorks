using AW.UI.Web.Infrastructure.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.Employee.Handlers.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, UpdatedEmployee>
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

        public async Task<UpdatedEmployee> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating employee");
            var updatedEmployee = await _client.UpdateEmployee(request.Employee);
            _logger.LogInformation("Employee succesfully updated");

            return updatedEmployee!;
        }
    }
}
