using AW.UI.Web.Infrastructure.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.Shift.Handlers.CreateShift
{
    public class CreateShiftCommandHandler : IRequestHandler<CreateShiftCommand, CreatedShift>
    {
        private readonly ILogger<CreateShiftCommandHandler> _logger;
        private readonly IShiftApiClient _client;

        public CreateShiftCommandHandler(ILogger<CreateShiftCommandHandler> logger, IShiftApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<CreatedShift> Handle(CreateShiftCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating shift");
            var shift = await _client.CreateShift(request.Shift);
            _logger.LogInformation("Shift succesfully added");

            return shift!;
        }
    }
}
