using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Product.Core.Common;
using AW.Services.Product.Core.Extensions;
using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Product.Core.Handlers.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, GetProductsDto>
    {
        private readonly ILogger<GetProductsQueryHandler> _logger;
        private readonly IRepository<Entities.Product> _repository;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(
            ILogger<GetProductsQueryHandler> logger,
            IRepository<Entities.Product> repository, 
            IMapper mapper) 
            => (_logger, _repository, _mapper) = (logger, repository, mapper);
        
        public async Task<GetProductsDto> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");
            _logger.LogInformation("Getting products from database");

            var spec = new GetProductsPaginatedSpecification(
                request.PageIndex, 
                request.PageSize,
                request.Category,
                request.Subcategory,
                OrderBy(request.OrderBy)
            );
            var countSpec = new CountProductsSpecification(
                request.Category,
                request.Subcategory
            );

            var products = await _repository.ListAsync(spec, cancellationToken);
            Guard.Against.Null(products, _logger);

            _logger.LogInformation("Returning products");
            return new GetProductsDto
            {
                Products = _mapper.Map<List<GetProduct.Product>>(products),
                TotalProducts = await _repository.CountAsync(countSpec, cancellationToken)
            };
        }

        private static OrderByClause<Entities.Product> OrderBy(string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy))
                return null;

            string direction = orderBy[..orderBy.IndexOf('(')];

            string propertyName = orderBy.Substring(
                orderBy.IndexOf('(') + 1,
                orderBy.IndexOf(')') - orderBy.IndexOf('(') - 1
            );

            var parameter = Expression.Parameter(typeof(Entities.Product));
            var memberExpression = Expression.Property(parameter, propertyName);
            var expr = Expression.Lambda(memberExpression, parameter);

            return new OrderByClause<Core.Entities.Product>
            {
                Expression = expr.ToUntypedPropertyExpression<Entities.Product>(),
                Direction = direction == "asc" ? OrderByDirection.Ascending : OrderByDirection.Descending
            };
        }
    }
}