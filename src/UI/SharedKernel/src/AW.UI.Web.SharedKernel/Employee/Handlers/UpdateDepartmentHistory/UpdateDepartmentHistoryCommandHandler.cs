using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Employee.Handlers.UpdateDepartmentHistory
{
    public class UpdateDepartmentHistoryCommandHandler : IRequestHandler<UpdateDepartmentHistoryCommand>
    {
        private readonly ILogger<UpdateDepartmentHistoryCommandHandler> _logger;
        private readonly IEmployeeApiClient _client;

        public UpdateDepartmentHistoryCommandHandler(ILogger<UpdateDepartmentHistoryCommandHandler> logger, IEmployeeApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task Handle(UpdateDepartmentHistoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Invoking Employee API to update department history");
            await _client.UpdateDepartmentHistory(request);

            _logger.LogInformation("Department history successfully updated");
        }
    }
}
