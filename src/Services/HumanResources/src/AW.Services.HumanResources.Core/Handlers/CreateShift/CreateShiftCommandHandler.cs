using AutoMapper;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.CreateShift
{
    public class CreateShiftCommandHandler : IRequestHandler<CreateShiftCommand, Shift>
    {
        private readonly ILogger<CreateShiftCommandHandler> _logger;
        private readonly IRepository<Entities.Shift> _shiftRepository;
        private readonly IMapper _mapper;

        public CreateShiftCommandHandler(
            ILogger<CreateShiftCommandHandler> logger,
            IRepository<Entities.Shift> shiftRepository,
            IMapper mapper
        )
        {
            _logger = logger;
            _shiftRepository = shiftRepository;
            _mapper = mapper;
        }

        public async Task<Shift> Handle(CreateShiftCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");

            _logger.LogInformation("Saving new shift to database");
            var shift = _mapper.Map<Entities.Shift>(request.Shift);
            shift = await _shiftRepository.AddAsync(shift, cancellationToken);

            _logger.LogInformation("Returning shift");
            return _mapper.Map<Shift>(shift);
        }
    }
}
