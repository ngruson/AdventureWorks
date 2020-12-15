using MediatR;

namespace AW.Application.SalesPerson.GetSalesPerson
{
    public class GetSalesPersonQuery : IRequest<SalesPersonDto>
    {
        public string FullName { get; set; }
    }
}