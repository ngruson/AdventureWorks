using Ardalis.GuardClauses;
using AutoMapper;
using AW.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.ReferenceData.Core.Handlers.ShipMethod.GetShipMethods
{
    public class GetShipMethodsQueryHandler : IRequestHandler<GetShipMethodsQuery, List<ShipMethod>>
    {
        private readonly ILogger<GetShipMethodsQueryHandler> logger;
        private readonly IMapper mapper;
        private readonly IRepository<Entities.ShipMethod> repository;

        public GetShipMethodsQueryHandler(
            ILogger<GetShipMethodsQueryHandler> logger,
            IRepository<Entities.ShipMethod> repository,
            IMapper mapper
        ) =>
            (this.logger, this.mapper, this.repository) = 
                (logger, mapper, repository);

        public async Task<List<ShipMethod>> Handle(GetShipMethodsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");

            logger.LogInformation("Getting shipping methods from database");
            var shipMethods = await repository.ListAsync();
            Guard.Against.NullOrEmpty(shipMethods, nameof(shipMethods));

            logger.LogInformation("Returning shipping methods");
            return mapper.Map<List<ShipMethod>>(shipMethods);
        }
    }
}