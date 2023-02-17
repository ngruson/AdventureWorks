using Ardalis.Specification;
using AW.Services.Customer.Core.Entities;

namespace AW.Services.Customer.Core.Specifications
{
    public class GetCustomersPaginatedSpecification : Specification<Entities.Customer>
    {
        public GetCustomersPaginatedSpecification(int pageIndex, int pageSize, CustomerType? customerType, string? territory, string? accountNumber) : base()
        {
            Query.Include(c => c.Addresses)
                .ThenInclude(a => a.Address);

            Query.Include("Person");
            Query.Include("Contacts.ContactPerson.EmailAddresses");
            Query.Include("Contacts.ContactPerson.PhoneNumbers");
            Query.Include(_ => _.SalesOrders);

            Query
                .Where(c =>
                    (string.IsNullOrEmpty(territory) || c.Territory == territory) &&
                    (!customerType.HasValue || (customerType == CustomerType.Individual ?
                        c is IndividualCustomer : c is StoreCustomer)
                    ) &&
                    (string.IsNullOrEmpty(accountNumber) || c.AccountNumber == accountNumber)
                )
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .OrderBy(c => c.AccountNumber);
        }
    }
}
