using System.Collections.Generic;

namespace AW.Core.Abstractions.Api.SalesPersonApi.ListSalesPersons
{
    public class ListSalesPersonsResponse
    {
        public List<SalesPerson> SalesPersons { get; set; } = new List<SalesPerson>();
    }
}