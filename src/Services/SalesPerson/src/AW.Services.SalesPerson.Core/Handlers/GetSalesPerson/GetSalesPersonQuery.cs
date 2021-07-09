using MediatR;

namespace AW.Services.SalesPerson.Core.Handlers.GetSalesPerson
{
    public class GetSalesPersonQuery : IRequest<SalesPersonDto>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}