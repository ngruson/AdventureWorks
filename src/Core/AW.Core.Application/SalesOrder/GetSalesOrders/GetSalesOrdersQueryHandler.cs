﻿using Ardalis.Specification;
using AutoMapper;
using AW.Core.Application.Specifications;
using AW.Core.Domain.Sales;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Core.Application.SalesOrder.GetSalesOrders
{
    public class GetSalesOrdersQueryHandler : IRequestHandler<GetSalesOrdersQuery, GetSalesOrdersDto>
    {
        private readonly IRepositoryBase<SalesOrderHeader> repository;
        private readonly IMapper mapper;

        public GetSalesOrdersQueryHandler(IRepositoryBase<SalesOrderHeader> repository, IMapper mapper) =>
            (this.repository, this.mapper) = (repository, mapper);

        public async Task<GetSalesOrdersDto> Handle(GetSalesOrdersQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetSalesOrdersPaginatedSpecification(
                request.PageIndex,
                request.PageSize,
                request.CustomerType,
                request.Territory
            );
            var countSpec = new CountSalesOrdersSpecification(
                request.CustomerType,
                request.Territory
            );

            var orders = await repository.ListAsync(spec);

            return new GetSalesOrdersDto
            {
                SalesOrders = mapper.Map<IEnumerable<SalesOrderDto>>(orders),
                TotalSalesOrders = await repository.CountAsync(countSpec)
            };
        }
    }
}