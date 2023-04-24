using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Department.Handlers.GetDepartment
{
    public class GetDepartmentQueryHandler : IRequestHandler<GetDepartmentQuery, Department>
    {
        private readonly ILogger<GetDepartmentQueryHandler> _logger;
        private readonly IDepartmentApiClient _client;

        public GetDepartmentQueryHandler(ILogger<GetDepartmentQueryHandler> logger, IDepartmentApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<Department> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting department from API");
            var department = await _client.GetDepartment(request.Name);
            Guard.Against.Null(department, _logger);

            _logger.LogInformation("Returning department");

            return department!;
        }
    }
}
