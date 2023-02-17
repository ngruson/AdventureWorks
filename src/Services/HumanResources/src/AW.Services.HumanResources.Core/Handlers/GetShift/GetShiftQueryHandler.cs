using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.HumanResources.Core.GuardClauses;
using AW.Services.HumanResources.Core.Handlers.GetEmployee;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.GetShift
{
    public class GetShiftQueryHandler : IRequestHandler<GetShiftQuery, Shift>
    {
        private readonly ILogger<GetShiftQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.Shift> _repository;

        public GetShiftQueryHandler(
            ILogger<GetShiftQueryHandler> logger,
            IMapper mapper,
            IRepository<Entities.Shift> repository
        ) => (_logger, _mapper, _repository) = (logger, mapper, repository);

        public async Task<Shift> Handle(GetShiftQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");
            _logger.LogInformation("Getting shift from database");

            var spec = new GetShiftSpecification(
                request.Name
            );

            var shift = await _repository.SingleOrDefaultAsync(spec, cancellationToken);
            Guard.Against.ShiftNull(shift, request.Name!, _logger);

            _logger.LogInformation("Returning shift");
            return _mapper.Map<Shift>(shift);
        }
    }
}
