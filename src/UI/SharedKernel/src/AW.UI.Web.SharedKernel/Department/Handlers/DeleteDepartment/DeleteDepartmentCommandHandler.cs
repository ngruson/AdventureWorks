using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Department.Handlers.DeleteDepartment
{
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand>
    {
        private readonly ILogger<DeleteDepartmentCommandHandler> _logger;
        private readonly IDepartmentApiClient _client;

        public DeleteDepartmentCommandHandler(
            ILogger<DeleteDepartmentCommandHandler> logger,
            IDepartmentApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(request.Name, _logger);

            _logger.LogInformation("Deleting department");
            await _client.DeleteDepartment(request);
            _logger.LogInformation("Department succesfully deleted");
        }
    }
}
