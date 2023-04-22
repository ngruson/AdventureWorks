using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Shift.Handlers.GetShifts
{
    public class GetShiftsQueryHandler : IRequestHandler<GetShiftsQuery, List<Shift>>
    {
        private readonly ILogger<GetShiftsQueryHandler> _logger;
        private readonly IShiftApiClient _client;

        public GetShiftsQueryHandler(ILogger<GetShiftsQueryHandler> logger, IShiftApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<List<Shift>> Handle(GetShiftsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting shifts from API");
            var shifts = await _client.GetShifts();
            Guard.Against.Null(shifts, _logger);

            _logger.LogInformation("Returning shifts");

            return shifts!;
        }
    }
}
