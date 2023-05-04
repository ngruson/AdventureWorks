using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.HumanResources.Core.GuardClauses;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.UpdateShift
{
    public class UpdateShiftCommandHandler : IRequestHandler<UpdateShiftCommand, Shift>
    {
        private readonly ILogger<UpdateShiftCommandHandler> _logger;
        private readonly IRepository<Entities.Shift> _shiftRepository;
        private readonly IMapper _mapper;

        public UpdateShiftCommandHandler(
            ILogger<UpdateShiftCommandHandler> logger,
            IRepository<Entities.Shift> shiftRepository,
            IMapper mapper
        )
        {
            _logger = logger;
            _shiftRepository = shiftRepository;
            _mapper = mapper;
        }

        public async Task<Shift> Handle(UpdateShiftCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");

            _logger.LogInformation("Getting shift from database");
            var spec = new GetShiftSpecification(request.Key);
            var shift = await _shiftRepository.SingleOrDefaultAsync(spec, cancellationToken);
            Guard.Against.ShiftNull(shift, request.Key, _logger);

            _logger.LogInformation("Updating shift");
            _mapper.Map(request.Shift, shift);

            _logger.LogInformation("Saving shift to database");
            await _shiftRepository.UpdateAsync(shift!, cancellationToken);

            _logger.LogInformation("Returning shift");
            return _mapper.Map<Shift>(shift);
        }
    }
}
