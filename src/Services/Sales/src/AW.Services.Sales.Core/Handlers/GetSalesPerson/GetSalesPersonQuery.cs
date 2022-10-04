using AW.SharedKernel.ValueTypes;
using MediatR;

namespace AW.Services.Sales.Core.Handlers.GetSalesPerson
{
    public class GetSalesPersonQuery : IRequest<SalesPersonDto>
    {
        public NameFactory Name { get; init; }
    }
}