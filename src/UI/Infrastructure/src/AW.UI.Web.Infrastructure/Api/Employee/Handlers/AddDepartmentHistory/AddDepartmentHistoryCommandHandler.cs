using AW.UI.Web.Infrastructure.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.Employee.Handlers.AddDepartmentHistory
{
    public class AddDepartmentHistoryCommandHandler : IRequestHandler<AddDepartmentHistoryCommand>
    {
        private readonly ILogger<AddDepartmentHistoryCommandHandler> _logger;
        private readonly IEmployeeApiClient _client;

        public AddDepartmentHistoryCommandHandler(ILogger<AddDepartmentHistoryCommandHandler> logger, IEmployeeApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task Handle(AddDepartmentHistoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Add department history calling API");
            await _client.AddDepartmentHistory(request);

            _logger.LogInformation("Add department history succeeded");
        }
    }
}
