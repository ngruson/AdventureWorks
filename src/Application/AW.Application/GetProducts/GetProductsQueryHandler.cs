using AutoMapper;
using AW.Application.Interfaces;
using AW.Application.Specifications;
using AW.Domain.Production;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IAsyncRepository<Product> repository;
        private readonly IMapper mapper;

        public GetProductsQueryHandler(IAsyncRepository<Product> repository, IMapper mapper) 
            => (this.repository, this.mapper) = (repository, mapper);
        
        public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetProductsPaginatedSpecification(request.PageIndex, request.PageSize);
            var products = await repository.ListAsync(spec);
            return mapper.Map<IEnumerable<ProductDto>>(products);
        }
    }
}