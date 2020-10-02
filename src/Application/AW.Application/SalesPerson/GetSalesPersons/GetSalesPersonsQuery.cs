using MediatR;
using System.Collections.Generic;

namespace AW.Application.SalesPerson.GetSalesPersons
{
    public class GetSalesPersonsQuery : IRequest<IEnumerable<SalesPersonDto>>
    {
        public string SalesTerritoryName { get; set; }
    }
}