using AW.UI.Web.Infrastructure.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.Employee.Handlers.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Employee>
    {
        private readonly ILogger<CreateEmployeeCommandHandler> _logger;
        private readonly IEmployeeApiClient _client;

        public CreateEmployeeCommandHandler(ILogger<CreateEmployeeCommandHandler> logger, IEmployeeApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating employee");
            var employee = await _client.CreateEmployee(request.Employee);
            _logger.LogInformation("Employee succesfully added");

            return employee!;
        }
    }
}
