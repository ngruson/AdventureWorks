using Ardalis.Specification;
using AW.Application.Specifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.Product.CountProducts
{
    public class CountProductsQueryHandler : IRequestHandler<CountProductsQuery, int>
    {
        private readonly IRepositoryBase<Domain.Production.Product> repository;

        public CountProductsQueryHandler(IRepositoryBase<Domain.Production.Product> repository)
            => (this.repository) = (repository);

        public async Task<int> Handle(CountProductsQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetProductsSpecification();
            var products = await repository.ListAsync(spec);
            return products.Count;
        }
    }
}