using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.HumanResources.Core.GuardClauses;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.GetShifts
{
    public class GetShiftsQueryHandler : IRequestHandler<GetShiftsQuery, List<Shift>>
    {
        private readonly ILogger<GetShiftsQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.Shift> _repository;

        public GetShiftsQueryHandler(
            ILogger<GetShiftsQueryHandler> logger,
            IMapper mapper,
            IRepository<Entities.Shift> repository
        ) => (_logger, _mapper, _repository) = (logger, mapper, repository);

        public async Task<List<Shift>> Handle(GetShiftsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");
            _logger.LogInformation("Getting shifts from database");

            var shifts = await _repository.ListAsync(cancellationToken);
            Guard.Against.ShiftsNull(shifts, _logger);

            _logger.LogInformation("Returning shifts");
            return _mapper.Map<List<Shift>>(shifts);
        }
    }
}