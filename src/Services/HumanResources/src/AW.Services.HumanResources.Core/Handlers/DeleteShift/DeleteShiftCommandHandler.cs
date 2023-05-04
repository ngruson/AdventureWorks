using Ardalis.GuardClauses;
using AW.Services.HumanResources.Core.GuardClauses;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.DeleteShift
{
    public class DeleteShiftCommandHandler : IRequestHandler<DeleteShiftCommand>
    {
        private readonly ILogger<DeleteShiftCommandHandler> _logger;
        private readonly IRepository<Entities.Shift> _shiftRepository;

        public DeleteShiftCommandHandler(
            ILogger<DeleteShiftCommandHandler> logger,
            IRepository<Entities.Shift> shiftRepository
        )
        {
            _logger = logger;
            _shiftRepository = shiftRepository;
        }

        public async Task Handle(DeleteShiftCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");

            _logger.LogInformation("Getting shift from database");
            var spec = new GetShiftSpecification(request.Name);
            var shift = await _shiftRepository.SingleOrDefaultAsync(spec, cancellationToken);
            Guard.Against.ShiftNull(shift, request.Name!, _logger);

            _logger.LogInformation("Deleting shift from database");
            await _shiftRepository.DeleteAsync(shift!, cancellationToken);

            _logger.LogInformation("Deleted shift from database");
        }
    }
}
