using MediatR;
using System.Collections.Generic;

namespace AW.Services.SalesPerson.Application.GetSalesPersons
{
    public class GetSalesPersonsQuery : IRequest<List<SalesPersonDto>>
    {
        public string Territory { get; set; }
    }
}