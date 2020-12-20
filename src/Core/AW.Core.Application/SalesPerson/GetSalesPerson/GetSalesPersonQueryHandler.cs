using Ardalis.Specification;
using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Core.Application.SalesPerson.GetSalesPerson
{
    public class GetSalesPersonQueryHandler : IRequestHandler<GetSalesPersonQuery, SalesPersonDto>
    {
        private readonly IRepositoryBase<Domain.Sales.SalesPerson> repository;
        private readonly IMapper mapper;

        public GetSalesPersonQueryHandler(IRepositoryBase<Domain.Sales.SalesPerson> repository, IMapper mapper) =>
            (this.repository, this.mapper) = (repository, mapper);

        public async Task<SalesPersonDto> Handle(GetSalesPersonQuery request, CancellationToken cancellationToken)
        {
            var salesPersons = await repository.ListAsync();
            var salesPerson = salesPersons.SingleOrDefault(sp => sp.FullName == request.FullName);                
            return mapper.Map<SalesPersonDto>(salesPerson);
        }
    }
}