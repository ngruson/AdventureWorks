using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Employee.Handlers.GetEmployee
{
    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, Employee>
    {
        private readonly ILogger<GetEmployeeQueryHandler> _logger;
        private readonly IEmployeeApiClient _client;

        public GetEmployeeQueryHandler(ILogger<GetEmployeeQueryHandler> logger, IEmployeeApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<Employee> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting employee {LoginID} from API", request.LoginID);
            var employee = await _client.GetEmployee(
                request.LoginID!
            );
            Guard.Against.Null(employee, _logger);

            _logger.LogInformation("Returning employee");

            return employee!;
        }
    }
}
