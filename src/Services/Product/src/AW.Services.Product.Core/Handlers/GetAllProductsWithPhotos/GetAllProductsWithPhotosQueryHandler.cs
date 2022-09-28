using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Product.Core.Handlers.GetAllProductsWithPhotos
{
    public class GetAllProductsWithPhotosQueryHandler : IRequestHandler<GetAllProductsWithPhotosQuery, List<ProductWithPhotoDto>>
    {
        private readonly ILogger<GetAllProductsWithPhotosQueryHandler> _logger;
        private readonly IRepository<Entities.Product> _repository;
        private readonly IMapper _mapper;

        public GetAllProductsWithPhotosQueryHandler(
            ILogger<GetAllProductsWithPhotosQueryHandler> logger,
            IRepository<Entities.Product> repository,
            IMapper mapper
        ) =>
            (_logger, _repository, _mapper) = (logger, repository, mapper);
        
        public async Task<List<ProductWithPhotoDto>> Handle(GetAllProductsWithPhotosQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetAllProductsWithPhotosSpecification();
            _logger.LogInformation("Getting products from database");
            var products = await _repository.ListAsync(spec, cancellationToken);

            Guard.Against.Null(products, _logger);

            _logger.LogInformation("Returning products");

            return _mapper.Map<List<ProductWithPhotoDto>>(products);
        }
    }
}