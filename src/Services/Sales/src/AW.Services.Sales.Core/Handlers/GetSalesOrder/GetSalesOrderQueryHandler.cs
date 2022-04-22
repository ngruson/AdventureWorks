﻿using AutoMapper;
using AW.Services.Sales.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrder
{
    public class GetSalesOrderQueryHandler : IRequestHandler<GetSalesOrderQuery, SalesOrderDto>
    {
        private readonly ILogger<GetSalesOrderQueryHandler> logger;
        private readonly IRepository<Entities.SalesOrder> repository;
        private readonly IMapper mapper;

        public GetSalesOrderQueryHandler(
            ILogger<GetSalesOrderQueryHandler> logger,
            IRepository<Entities.SalesOrder> repository, IMapper mapper) =>
            (this.logger, this.repository, this.mapper) = (logger, repository, mapper);

        public async Task<SalesOrderDto> Handle(GetSalesOrderQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");

            logger.LogInformation("Getting sales order from database");
            var spec = new GetFullSalesOrderSpecification(
                request.SalesOrderNumber
            );

            var salesOrder = await repository.GetBySpecAsync(spec, cancellationToken);

            logger.LogInformation("Returning sales orders");
            return mapper.Map<SalesOrderDto>(salesOrder);
        }
    }
}