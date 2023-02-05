using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.ReferenceData.Core.GuardClauses;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.ReferenceData.Core.Handlers.ShipMethod.GetShipMethods
{
    public class GetShipMethodsQueryHandler : IRequestHandler<GetShipMethodsQuery, List<ShipMethod>>
    {
        private readonly ILogger<GetShipMethodsQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.ShipMethod> _repository;

        public GetShipMethodsQueryHandler(
            ILogger<GetShipMethodsQueryHandler> logger,
            IRepository<Entities.ShipMethod> repository,
            IMapper mapper
        ) =>
            (_logger, _mapper, _repository) = (logger, mapper, repository);

        public async Task<List<ShipMethod>> Handle(GetShipMethodsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");

            _logger.LogInformation("Getting shipping methods from database");
            var shipMethods = await _repository.ListAsync(cancellationToken);
            Guard.Against.ShipMethodsNull(shipMethods, _logger);

            _logger.LogInformation("Returning shipping methods");
            return _mapper.Map<List<ShipMethod>>(shipMethods).OrderBy(_ => _.Name).ToList();
        }
    }
}