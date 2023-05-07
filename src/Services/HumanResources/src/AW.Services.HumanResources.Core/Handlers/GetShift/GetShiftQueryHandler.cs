using Ardalis.GuardClauses;
using Ardalis.Result;
using AutoMapper;
using AW.Services.HumanResources.Core.GuardClauses;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.GetShift
{
    public class GetShiftQueryHandler : IRequestHandler<GetShiftQuery, Result<Shift>>
    {
        private readonly ILogger<GetShiftQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.Shift> _repository;

        public GetShiftQueryHandler(
            ILogger<GetShiftQueryHandler> logger,
            IMapper mapper,
            IRepository<Entities.Shift> repository
        ) => (_logger, _mapper, _repository) = (logger, mapper, repository);

        public async Task<Result<Shift>> Handle(GetShiftQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");
            _logger.LogInformation("Getting shift from database");

            var spec = new GetShiftSpecification(
                request.ObjectId
            );

            var shift = await _repository.SingleOrDefaultAsync(spec, cancellationToken);
            var result = Guard.Against.ShiftNull(shift, request.ObjectId, _logger);
            if (!result.IsSuccess)
                return result;

            _logger.LogInformation("Returning shift");
            return Result.Success(
                _mapper.Map<Shift>(shift)
            );
        }
    }
}
