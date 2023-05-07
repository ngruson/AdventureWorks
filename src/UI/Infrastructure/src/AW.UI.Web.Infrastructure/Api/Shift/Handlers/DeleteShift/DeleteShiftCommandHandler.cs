using AW.UI.Web.Infrastructure.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.Shift.Handlers.DeleteShift
{
    public class DeleteShiftCommandHandler : IRequestHandler<DeleteShiftCommand>
    {
        private readonly ILogger<DeleteShiftCommandHandler> _logger;
        private readonly IShiftApiClient _client;

        public DeleteShiftCommandHandler(
            ILogger<DeleteShiftCommandHandler> logger,
            IShiftApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task Handle(DeleteShiftCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting shift");
            await _client.DeleteShift(request.ObjectId);
            _logger.LogInformation("Shift succesfully deleted");
        }
    }
}
