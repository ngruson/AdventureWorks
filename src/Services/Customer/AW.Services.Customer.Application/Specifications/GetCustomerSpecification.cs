using Ardalis.Specification;
using System.Linq;

namespace AW.Services.Customer.Application.Specifications
{
    public class GetCustomerSpecification : Specification<Domain.Customer>
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