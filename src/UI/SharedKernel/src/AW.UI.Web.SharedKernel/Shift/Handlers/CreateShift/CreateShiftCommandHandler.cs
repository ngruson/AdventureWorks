using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Shift.Handlers.CreateShift
{
    public class CreateShiftCommandHandler : IRequestHandler<CreateShiftCommand, Shift>
    {
        private readonly ILogger<CreateShiftCommandHandler> _logger;
        private readonly IShiftApiClient _client;

        public CreateShiftCommandHandler(ILogger<CreateShiftCommandHandler> logger, IShiftApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<Shift> Handle(CreateShiftCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating shift");
            var shift = await _client.CreateShift(request.Shift);
            _logger.LogInformation("Shift succesfully added");

            return shift!;
        }
    }
}
