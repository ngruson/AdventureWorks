using MediatR;
using System.Collections.Generic;

namespace AW.Services.SalesPerson.Core.Handlers.GetSalesPersons
{
    public class GetSalesPersonsQuery : IRequest<List<GetSalesPerson.SalesPersonDto>>
    {
        public string Territory { get; set; }
    }
}