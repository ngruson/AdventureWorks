using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AW.Services.HumanResources.Core.GuardClauses;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.DeleteShift
{
    public class DeleteShiftCommandHandler : IRequestHandler<DeleteShiftCommand, Result>
    {
        private readonly ILogger<DeleteShiftCommandHandler> _logger;
        private readonly IRepository<Entities.Shift> _shiftRepository;
        private readonly IValidator<DeleteShiftCommand> _validator;

        public DeleteShiftCommandHandler(
            ILogger<DeleteShiftCommandHandler> logger,
            IRepository<Entities.Shift> shiftRepository,
            IValidator<DeleteShiftCommand> validator
        )
        {
            _logger = logger;
            _shiftRepository = shiftRepository;
            _validator = validator;
        }

        public async Task<Result> Handle(DeleteShiftCommand request, CancellationToken cancellationToken)
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
                var spec = new GetShiftSpecification(request.ObjectId);
                var shift = await _shiftRepository.SingleOrDefaultAsync(spec, cancellationToken);
                var result = Guard.Against.ShiftNull(shift, request.ObjectId, _logger);
                if (!result.IsSuccess)
                    return result;

                _logger.LogInformation("Deleting shift from database");
                await _shiftRepository.DeleteAsync(shift!, cancellationToken);

                _logger.LogInformation("Deleted shift from database");

                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                return Result.Error(ex.Message);
            }
        }
    }
}
