using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Shift.Handlers.GetShift
{
    public class GetShiftQueryHandler : IRequestHandler<GetShiftQuery, Shift>
    {
        private readonly ILogger<GetShiftQueryHandler> _logger;
        private readonly IShiftApiClient _client;

        public GetShiftQueryHandler(ILogger<GetShiftQueryHandler> logger, IShiftApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<Shift> Handle(GetShiftQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting shift from API");
            var shift = await _client.GetShift(request.Name);
            Guard.Against.Null(shift, _logger);

            _logger.LogInformation("Returning shift");

            return shift!;
        }
    }
}
