using MediatR;
using System.Collections.Generic;

namespace AW.Services.SalesPerson.Core.Handlers.GetSalesPersons
{
    public class GetSalesPersonsQuery : IRequest<List<SalesPersonDto>>
    {
        public string Territory { get; set; }
    }
}