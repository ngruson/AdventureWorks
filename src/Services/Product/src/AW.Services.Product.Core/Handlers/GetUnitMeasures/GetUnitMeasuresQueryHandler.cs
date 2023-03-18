using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Product.Core.GuardClauses;
using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Product.Core.Handlers.GetUnitMeasures
{
    public class GetUnitMeasuresQueryHandler : IRequestHandler<GetUnitMeasuresQuery, List<UnitMeasure>>
    {
        private readonly ILogger<GetUnitMeasuresQueryHandler> _logger;
        private readonly IRepository<Entities.UnitMeasure> _repository;
        private readonly IMapper _mapper;

        public GetUnitMeasuresQueryHandler(
            ILogger<GetUnitMeasuresQueryHandler> logger,
            IRepository<Entities.UnitMeasure> repository,
            IMapper mapper)
            => (_logger, _repository, _mapper) = (logger, repository, mapper);

        public async Task<List<UnitMeasure>> Handle(GetUnitMeasuresQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");
            _logger.LogInformation("Getting unit measures from database");

            var spec = new GetUnitMeasuresSpecification();
            var unitMeasures = await _repository.ListAsync(spec, cancellationToken);
            Guard.Against.UnitMeasuresNullOrEmpty(unitMeasures, _logger);

            _logger.LogInformation("Returning unit measures");
            return _mapper.Map<List<UnitMeasure>>(unitMeasures);
        }
    }
}
