using AW.SharedKernel.ValueTypes;
using MediatR;

namespace AW.Services.Sales.Core.Handlers.GetSalesPerson
{
    public class GetSalesPersonQuery : IRequest<SalesPersonDto?>
    {
        public GetSalesPersonQuery(NameFactory name)
        {
            Name = name;
        }

        public NameFactory Name { get; private init; }
    }
}