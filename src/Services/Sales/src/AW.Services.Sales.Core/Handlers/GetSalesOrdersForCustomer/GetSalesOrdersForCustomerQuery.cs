using MediatR;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrdersForCustomer
{
    public class GetSalesOrdersForCustomerQuery : IRequest<GetSalesOrdersDto>
    {
        public GetSalesOrdersForCustomerQuery(string customerNumber)
        {
            CustomerNumber = customerNumber;
        }

        public string CustomerNumber { get; private init; }
    }
}