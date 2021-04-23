using Ardalis.Specification;
using AW.Services.Customer.Domain;
using System.Linq;

namespace AW.Services.Customer.Application.Specifications
{
    public class GetCustomersPaginatedSpecification : Specification<Domain.Customer>
    {
        public GetCustomersPaginatedSpecification(int pageIndex, int pageSize, CustomerType? customerType, string territory, string accountNumber) : base()
        {
            Query.Include(c => c.Addresses)
                .ThenInclude(a => a.Address);

            //Query.Include(c => (c as IndividualCustomer).Person)
            //    .ThenInclude(p => p.PhoneNumbers);
            Query.Include("Person");
            Query.Include("Contacts.ContactPerson.EmailAddresses");
            Query.Include("Contacts.ContactPerson.PhoneNumbers");

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