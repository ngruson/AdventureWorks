using MediatR;

namespace AW.Core.Application.SalesPerson.GetSalesPerson
{
    public class GetSalesPersonQuery : IRequest<SalesPersonDto>
    {
        public string FullName { get; set; }
    }
}