using Ardalis.GuardClauses;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.SharedKernel.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Employee.Handlers.GetJobTitles
{
    public class GetJobTitlesQueryHandler : IRequestHandler<GetJobTitlesQuery, List<string>>
    {
        private readonly ILogger<GetJobTitlesQueryHandler> _logger;
        private readonly IEmployeeApiClient _client;

        public GetJobTitlesQueryHandler(ILogger<GetJobTitlesQueryHandler> logger, IEmployeeApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<List<string>> Handle(GetJobTitlesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting job titles from API");
            var jobTitles = await _client.GetJobTitles();
            Guard.Against.Null(jobTitles, _logger);

            _logger.LogInformation("Returning job titles");

            return jobTitles!;
        }
    }
}
