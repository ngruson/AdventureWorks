using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.Employee.Handlers.GetEmployees
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<Employee>>
    {
        private readonly ILogger<GetEmployeesQueryHandler> _logger;
        private readonly IEmployeeApiClient _client;

        public GetEmployeesQueryHandler(ILogger<GetEmployeesQueryHandler> logger, IEmployeeApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<List<Employee>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting employees from API");
            var employees = await _client.GetEmployees();
            Guard.Against.Null(employees, _logger);

            _logger.LogInformation("Returning employees");

            return employees!;
        }
    }
}
