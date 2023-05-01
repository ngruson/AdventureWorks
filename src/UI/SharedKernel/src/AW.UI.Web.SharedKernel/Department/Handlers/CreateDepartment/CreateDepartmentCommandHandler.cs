using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Department.Handlers.CreateDepartment
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Department>
    {
        private readonly ILogger<CreateDepartmentCommandHandler> _logger;
        private readonly IDepartmentApiClient _client;

        public CreateDepartmentCommandHandler(ILogger<CreateDepartmentCommandHandler> logger, IDepartmentApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<Department> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating department");
            var department = await _client.CreateDepartment(request.Department);
            _logger.LogInformation("Department succesfully added");

            return department!;
        }
    }
}
