using Ardalis.Specification;
using System.Linq;

namespace AW.Services.Customer.Application.Specifications
{
    #if NETSTANDARD2_0
    public class GetCustomerSpecification : Specification<Domain.Customer>
    #elif NETSTANDARD2_1
    public class GetCustomerSpecification : Specification<Domain.Customer>, ISingleResultSpecification
    #endif
    {
        public GetCustomerSpecification(string accountNumber) : base()
        {
            Query.Include(c => c.Addresses)
                .ThenInclude(a => a.Address);
            Query.Include("Contacts.ContactPerson.EmailAddresses");
            Query.Include("Contacts.ContactPerson.PhoneNumbers");
            Query.Include(c => c.SalesOrders);

            Query
                .Where(c => c.AccountNumber == accountNumber);

        }
    }
}