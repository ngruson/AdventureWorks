﻿using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.Shift.Handlers.GetShift
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
            var shift = await _client.GetShift(request.ObjectId);
            Guard.Against.Null(shift, _logger);

            _logger.LogInformation("Returning shift");

            return shift!;
        }
    }
}
