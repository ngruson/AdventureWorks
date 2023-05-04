using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Shift.Handlers.UpdateShift
{
    public class UpdateShiftCommandHandler : IRequestHandler<UpdateShiftCommand>
    {
        private readonly ILogger<UpdateShiftCommandHandler> _logger;
        private readonly IShiftApiClient _client;

        public UpdateShiftCommandHandler(
            ILogger<UpdateShiftCommandHandler> logger,
            IShiftApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task Handle(UpdateShiftCommand request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request.Shift, _logger);

            _logger.LogInformation("Updating shift");
            await _client.UpdateShift(request);
            _logger.LogInformation("Shift succesfully updated");
        }
    }
}
