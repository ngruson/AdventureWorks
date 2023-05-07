using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.Department.Handlers.UpdateDepartment
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand>
    {
        private readonly ILogger<UpdateDepartmentCommandHandler> _logger;
        private readonly IDepartmentApiClient _client;

        public UpdateDepartmentCommandHandler(
            ILogger<UpdateDepartmentCommandHandler> logger,
            IDepartmentApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request.Department, _logger);

            _logger.LogInformation("Updating department");
            await _client.UpdateDepartment(request);
            _logger.LogInformation("Department succesfully updated");
        }
    }
}
