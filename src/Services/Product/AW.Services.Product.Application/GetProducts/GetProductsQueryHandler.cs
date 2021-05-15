using Ardalis.GuardClauses;
using Ardalis.Specification;
using AutoMapper;
using AW.Services.Product.Application.Common;
using AW.Services.Product.Application.Extensions;
using AW.Services.Product.Application.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Product.Application.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, GetProductsDto>
    {
        private readonly ILogger<GetProductsQueryHandler> logger;
        private readonly IRepositoryBase<Domain.Product> repository;
        private readonly IMapper mapper;

        public GetProductsQueryHandler(
            ILogger<GetProductsQueryHandler> logger,
            IRepositoryBase<Domain.Product> repository, 
            IMapper mapper) 
            => (this.logger, this.repository, this.mapper) = (logger, repository, mapper);
        
        public async Task<GetProductsDto> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");
            logger.LogInformation("Getting products from database");

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

            var products = await repository.ListAsync(spec);

            Guard.Against.Null(products, nameof(products));

            logger.LogInformation("Returning products");
            return new GetProductsDto
            {
                Products = mapper.Map<List<Product>>(products),
                TotalProducts = await repository.CountAsync(countSpec)
            };
        }

        private OrderByClause<Domain.Product> OrderBy(string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy))
                return null;

            string direction = orderBy.Substring(0, orderBy.IndexOf('('));

            string propertyName = orderBy.Substring(
                orderBy.IndexOf('(') + 1,
                orderBy.IndexOf(')') - orderBy.IndexOf('(') - 1
            );

            var parameter = Expression.Parameter(typeof(Domain.Product));
            var memberExpression = Expression.Property(parameter, propertyName);
            var expr = Expression.Lambda(memberExpression, parameter);

            return new OrderByClause<Domain.Product>
            {
                Expression = expr.ToUntypedPropertyExpression<Domain.Product>(),
                Direction = direction == "asc" ? OrderByDirection.Ascending : OrderByDirection.Descending
            };
        }
    }
}