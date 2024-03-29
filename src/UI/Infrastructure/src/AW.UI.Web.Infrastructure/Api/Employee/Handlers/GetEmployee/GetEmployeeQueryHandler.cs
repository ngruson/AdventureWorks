﻿using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.Employee.Handlers.GetEmployee
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
            _logger.LogInformation("Getting employee {ObjectId} from API", request.ObjectId);
            var employee = await _client.GetEmployee(request.ObjectId);
            Guard.Against.Null(employee, _logger);

            _logger.LogInformation("Returning employee");

            return employee!;
        }
    }
}
