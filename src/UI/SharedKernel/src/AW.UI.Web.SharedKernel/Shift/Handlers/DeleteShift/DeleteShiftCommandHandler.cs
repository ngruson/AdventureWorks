using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Shift.Handlers.DeleteShift
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
            Guard.Against.NullOrEmpty(request.Name, _logger);

            _logger.LogInformation("Deleting shift");
            await _client.DeleteShift(request);
            _logger.LogInformation("Shift succesfully deleted");
        }
    }
}
