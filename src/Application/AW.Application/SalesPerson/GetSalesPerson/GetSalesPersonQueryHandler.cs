using AutoMapper;
using AW.Application.Interfaces;
using AW.Application.Specifications;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.SalesPerson.GetSalesPerson
{
    public class GetSalesPersonQueryHandler : IRequestHandler<GetSalesPersonQuery, SalesPersonDto>
    {
        private readonly IAsyncRepository<Domain.Sales.SalesPerson> repository;
        private readonly IMapper mapper;

        public GetSalesPersonQueryHandler(IAsyncRepository<Domain.Sales.SalesPerson> repository, IMapper mapper) =>
            (this.repository, this.mapper) = (repository, mapper);

        public async Task<SalesPersonDto> Handle(GetSalesPersonQuery request, CancellationToken cancellationToken)
        {
            var salesPersons = await repository.ListAllAsync();
            var salesPerson = salesPersons.SingleOrDefault(sp => sp.FullName == request.FullName);                
            return mapper.Map<SalesPersonDto>(salesPerson);
        }
    }
}