using Ardalis.Specification;
using AW.Core.Application.Specifications;
using AW.Core.Domain.Person;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Core.Application.Customer.UpdateCustomerContact
{
    public class UpdateCustomerContactCommandHandler : IRequestHandler<UpdateCustomerContactCommand, Unit>
    {
        private readonly IRepositoryBase<Domain.Sales.Customer> customerRepository;
        private readonly IRepositoryBase<Person> personRepository;

        public UpdateCustomerContactCommandHandler(
            IRepositoryBase<Domain.Sales.Customer> customerRepository,
            IRepositoryBase<Person> personRepository
        ) => (this.customerRepository, this.personRepository) = (customerRepository, personRepository);

        public async Task<Unit> Handle(UpdateCustomerContactCommand request, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetBySpecAsync(
                new GetCustomerSpecification(request.AccountNumber)
            );

            var customerContact = customer.Store.BusinessEntityContacts.FirstOrDefault(c =>
                c.ContactType.Name == request.CustomerContact.ContactTypeName &&
                c.Person.FirstName == request.CustomerContact.Contact.FirstName &&
                c.Person.MiddleName == request.CustomerContact.Contact.MiddleName &&
                c.Person.LastName == request.CustomerContact.Contact.LastName
            );

            customerContact.Person.Title = request.CustomerContact.Contact.Title;
            customerContact.Person.Suffix = request.CustomerContact.Contact.Suffix;
            customerContact.ModifiedDate = DateTime.Now;
            customerContact.rowguid = Guid.NewGuid();

            await customerRepository.UpdateAsync(customer);

            await UpdateEmailAddresses(request);

            return Unit.Value;
        }

        private async Task UpdateEmailAddresses(UpdateCustomerContactCommand request)
        {
            var person = await personRepository.GetBySpecAsync(
                new GetPersonSpecification(
                    request.CustomerContact.Contact.FirstName,
                    request.CustomerContact.Contact.MiddleName,
                    request.CustomerContact.Contact.LastName
                )
            );

            request.CustomerContact.Contact.EmailAddresses.ForEach(x =>
            {
                var emailAddress = person.EmailAddresses.SingleOrDefault(e => e.EmailAddress1 == x.EmailAddress);

                if (emailAddress == null)
                    person.EmailAddresses.Add(new EmailAddress
                    {
                        EmailAddress1 = x.EmailAddress
                    });
            });

            await personRepository.UpdateAsync(person);
        }
    }
}