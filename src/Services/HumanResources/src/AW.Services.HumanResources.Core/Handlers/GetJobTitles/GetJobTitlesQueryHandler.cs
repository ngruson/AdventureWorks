using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.GetJobTitles
{
    public class GetJobTitlesQueryHandler : IRequestHandler<GetJobTitlesQuery, List<string>>
    {
        private readonly ILogger<GetJobTitlesQueryHandler> _logger;
        private readonly IRepository<Entities.Employee> _repository;

        public GetJobTitlesQueryHandler(
            ILogger<GetJobTitlesQueryHandler> logger,
            IRepository<Entities.Employee> repository
        )
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<List<string>> Handle(GetJobTitlesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting job titles", request);

            var spec = new GetJobTitlesSpecification();

            var jobTitles = await _repository.ListAsync(spec, cancellationToken);

            _logger.LogInformation("Returning job titles");

            return jobTitles.Distinct().Order().ToList();
        }
    }
}
