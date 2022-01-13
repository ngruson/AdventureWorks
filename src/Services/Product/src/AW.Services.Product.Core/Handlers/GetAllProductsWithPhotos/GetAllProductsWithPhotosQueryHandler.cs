using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Product.Core.Specifications;
using AW.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Product.Core.Handlers.GetAllProductsWithPhotos
{
    public class GetAllProductsWithPhotosQueryHandler : IRequestHandler<GetAllProductsWithPhotosQuery, List<ProductWithPhotoDto>>
    {
        private readonly ILogger<GetAllProductsWithPhotosQueryHandler> logger;
        private readonly IRepository<Entities.Product> repository;
        private readonly IMapper mapper;

        public GetAllProductsWithPhotosQueryHandler(
            ILogger<GetAllProductsWithPhotosQueryHandler> logger,
            IRepository<Entities.Product> repository,
            IMapper mapper
        ) =>
            (this.logger, this.repository, this.mapper) = (logger, repository, mapper);
        
        public async Task<List<ProductWithPhotoDto>> Handle(GetAllProductsWithPhotosQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetAllProductsWithPhotosSpecification();
            logger.LogInformation("Getting products from database");
            var products = await repository.ListAsync(spec, cancellationToken);

            Guard.Against.Null(products, nameof(products));

            logger.LogInformation("Returning products");

            return mapper.Map<List<ProductWithPhotoDto>>(products);
        }
    }
}