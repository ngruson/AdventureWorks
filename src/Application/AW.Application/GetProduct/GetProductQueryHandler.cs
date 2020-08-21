using AutoMapper;
using AW.Application.Interfaces;
using AW.Application.Specifications;
using AW.Domain.Production;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.GetProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
    {
        private readonly IAsyncRepository<Product> repository;
        private readonly IMapper mapper;

        public GetProductQueryHandler(IAsyncRepository<Product> repository, IMapper mapper)
            => (this.repository, this.mapper) = (repository, mapper);

        public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetProductSpecification(request.ProductNumber);
            var product = await repository.FirstOrDefaultAsync(spec);
            return mapper.Map<ProductDto>(product);
        }
    }
}