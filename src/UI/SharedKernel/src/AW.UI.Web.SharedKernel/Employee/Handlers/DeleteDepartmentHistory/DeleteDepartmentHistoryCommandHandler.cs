using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Employee.Handlers.DeleteDepartmentHistory
{
    public class DeleteDepartmentHistoryCommandHandler : IRequestHandler<DeleteDepartmentHistoryCommand>
    {
        private readonly ILogger<DeleteDepartmentHistoryCommandHandler> _logger;
        private readonly IEmployeeApiClient _client;

        public DeleteDepartmentHistoryCommandHandler(ILogger<DeleteDepartmentHistoryCommandHandler> logger, IEmployeeApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task Handle(DeleteDepartmentHistoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Delete department history calling API");
            await _client.DeleteDepartmentHistory(request);

            _logger.LogInformation("Delete department history succeeded");
        }
    }
}
