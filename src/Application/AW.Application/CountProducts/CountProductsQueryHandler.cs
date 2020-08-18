using AW.Application.Interfaces;
using AW.Application.Specifications;
using AW.Domain.Production;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.CountProducts
{
    public class CountProductsQueryHandler : IRequestHandler<CountProductsQuery, int>
    {
        private readonly IAsyncRepository<Product> repository;

        public CountProductsQueryHandler(IAsyncRepository<Product> repository)
            => (this.repository) = (repository);

        public async Task<int> Handle(CountProductsQuery request, CancellationToken cancellationToken)
        {
            var spec = new ProductFilterSpecification();
            var products = await repository.ListAsync(spec);
            return products.Count;
        }
    }
}