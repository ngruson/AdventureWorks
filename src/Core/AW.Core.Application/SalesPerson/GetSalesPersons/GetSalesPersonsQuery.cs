using MediatR;
using System.Collections.Generic;

namespace AW.Core.Application.SalesPerson.GetSalesPersons
{
    public class GetSalesPersonsQuery : IRequest<IEnumerable<SalesPersonDto>>
    {
        public string SalesTerritoryName { get; set; }
    }
}