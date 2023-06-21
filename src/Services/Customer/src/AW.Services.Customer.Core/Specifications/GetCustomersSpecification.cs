using Ardalis.Specification;
using AW.Services.Customer.Core.Entities;

namespace AW.Services.Customer.Core.Specifications
{
    public class GetCustomersSpecification : Specification<Entities.Customer>
    {
        public GetCustomersSpecification(CustomerType? customerType, bool includeDetails = true) : base()
        {
            Query.Include("Person");

            if (includeDetails)
            {
                Query.Include(c => c.Addresses)
                    .ThenInclude(a => a.Address);

                Query.Include("Person.EmailAddresses");
                Query.Include("Contacts.ContactPerson.EmailAddresses");
                Query.Include("Contacts.ContactPerson.PhoneNumbers");
            }

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
