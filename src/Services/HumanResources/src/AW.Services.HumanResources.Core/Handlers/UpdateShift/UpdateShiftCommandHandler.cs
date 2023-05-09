using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using AW.Services.HumanResources.Core.GuardClauses;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.UpdateShift
{
    public class UpdateShiftCommandHandler : IRequestHandler<UpdateShiftCommand, Result<Shift>>
    {
        private readonly ILogger<UpdateShiftCommandHandler> _logger;
        private readonly IRepository<Entities.Shift> _shiftRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateShiftCommand> _validator;

        public UpdateShiftCommandHandler(
            ILogger<UpdateShiftCommandHandler> logger,
            IRepository<Entities.Shift> shiftRepository,
            IMapper mapper,
            IValidator<UpdateShiftCommand> validator
        )
        {
            _logger = logger;
            _shiftRepository = shiftRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<Shift>> Handle(UpdateShiftCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Validating command");

                var validation = await _validator.ValidateAsync(request, cancellationToken);
                if (!validation.IsValid)
                {
                    return Result.Invalid(validation.AsErrors());
                }

                _logger.LogInformation("Getting shift from database");
                var spec = new GetShiftSpecification(request.Shift.ObjectId);
                var shift = await _shiftRepository.SingleOrDefaultAsync(spec, cancellationToken);
                var result = Guard.Against.ShiftNull(shift, request.Shift.ObjectId, _logger);
                if (!result.IsSuccess)
                    return result;

                _logger.LogInformation("Updating shift");
                _mapper.Map(request.Shift, shift);

                _logger.LogInformation("Saving shift to database");
                await _shiftRepository.UpdateAsync(shift!, cancellationToken);

                _logger.LogInformation("Returning shift");
                return Result.Success(
                    _mapper.Map<Shift>(shift)
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
