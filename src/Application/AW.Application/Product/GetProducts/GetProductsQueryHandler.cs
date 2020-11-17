using Ardalis.Specification;
using AutoMapper;
using AW.Application.Specifications;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.Product.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IRepositoryBase<Domain.Production.Product> repository;
        private readonly IMapper mapper;

        public GetProductsQueryHandler(IRepositoryBase<Domain.Production.Product> repository, IMapper mapper) 
            => (this.repository, this.mapper) = (repository, mapper);
        
        public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetProductsPaginatedSpecification(request.PageIndex, request.PageSize);
            var products = await repository.ListAsync(spec);
            return mapper.Map<IEnumerable<ProductDto>>(products);
        }
    }
}