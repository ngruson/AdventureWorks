using Ardalis.Specification;
using AW.Services.Customer.Core.Entities;
using System.Linq;

namespace AW.Services.Customer.Core.Specifications
{
    public class GetAllCustomersSpecification : Specification<Entities.Customer>
    {
        public GetAllCustomersSpecification(CustomerType? customerType) : base()
        {
            Query.Include(c => c.Addresses)
                .ThenInclude(a => a.Address);

            Query.Include("Person");
            Query.Include("Person.EmailAddresses");
            Query.Include("Contacts.ContactPerson.EmailAddresses");
            Query.Include("Contacts.ContactPerson.PhoneNumbers");

            Query
                .Where(c =>
                    (!customerType.HasValue || (customerType == CustomerType.Individual ?
                        c is IndividualCustomer : c is StoreCustomer)
                    )
                )
                .OrderBy(c => c.AccountNumber);
        }
    }
}