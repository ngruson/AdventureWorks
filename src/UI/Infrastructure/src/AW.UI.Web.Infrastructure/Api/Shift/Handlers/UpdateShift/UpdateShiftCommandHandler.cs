using AW.UI.Web.Infrastructure.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.Shift.Handlers.UpdateShift
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
            _logger.LogInformation("Updating shift");
            await _client.UpdateShift(request.Shift);
            _logger.LogInformation("Shift succesfully updated");
        }
    }
}
