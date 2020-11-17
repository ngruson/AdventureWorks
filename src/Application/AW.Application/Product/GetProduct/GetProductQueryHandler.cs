using Ardalis.Specification;
using AutoMapper;
using AW.Application.Specifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.Product.GetProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
    {
        private readonly IRepositoryBase<Domain.Production.Product> repository;
        private readonly IMapper mapper;

        public GetProductQueryHandler(IRepositoryBase<Domain.Production.Product> repository, IMapper mapper)
            => (this.repository, this.mapper) = (repository, mapper);

        public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetProductSpecification(request.ProductNumber);
            var product = await repository.GetBySpecAsync(spec);
            return mapper.Map<ProductDto>(product);
        }
    }
}