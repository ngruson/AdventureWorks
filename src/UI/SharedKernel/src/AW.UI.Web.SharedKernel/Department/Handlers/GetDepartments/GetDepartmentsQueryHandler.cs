using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Department.Handlers.GetDepartments
{
    public class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, List<Department>>
    {
        private readonly ILogger<GetDepartmentsQueryHandler> _logger;
        private readonly IDepartmentApiClient _client;

        public GetDepartmentsQueryHandler(ILogger<GetDepartmentsQueryHandler> logger, IDepartmentApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<List<Department>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting departments from API");
            var departments = await _client.GetDepartments();
            Guard.Against.Null(departments, _logger);

            _logger.LogInformation("Returning departments");

            return departments!;
        }
    }
}
