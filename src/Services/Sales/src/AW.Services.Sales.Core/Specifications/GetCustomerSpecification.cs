using Ardalis.Specification;
using System.Linq;

namespace AW.Services.Sales.Core.Specifications
{
    public class GetCustomerSpecification : Specification<Entities.Customer>, ISingleResultSpecification<Entities.Customer>
    {
        public GetCustomerSpecification(string customerNumber)
        {
            Query.Where(_ => _.CustomerNumber == customerNumber);
        }
    }
}