using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.CreateShift
{
    public class CreateShiftCommandHandler : IRequestHandler<CreateShiftCommand, Result<CreatedShift>>
    {
        private readonly ILogger<CreateShiftCommandHandler> _logger;
        private readonly IRepository<Entities.Shift> _shiftRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateShiftCommand> _validator;

        public CreateShiftCommandHandler(
            ILogger<CreateShiftCommandHandler> logger,
            IRepository<Entities.Shift> shiftRepository,
            IMapper mapper,
            IValidator<CreateShiftCommand> validator
        )
        {
            _logger = logger;
            _shiftRepository = shiftRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<CreatedShift>> Handle(CreateShiftCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Validating command");

                var validation = await _validator.ValidateAsync(request, cancellationToken);
                if (!validation.IsValid)
                {
                    return Result.Invalid(validation.AsErrors());
                }

                _logger.LogInformation("Saving new shift to database");
                var shift = _mapper.Map<Entities.Shift>(request.Shift);
                shift = await _shiftRepository.AddAsync(shift, cancellationToken);

                _logger.LogInformation("Returning shift");
                return Result.Success(
                    _mapper.Map<CreatedShift>(shift)
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                return Result.Error(ex.Message);
            }
        }
    }
}
