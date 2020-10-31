using AutoMapper;
using AW.Application.Interfaces;
using AW.Application.Specifications;
using AW.Domain.Person;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.Customer.UpdateCustomerContact
{
    public class UpdateCustomerContactCommandHandler : IRequestHandler<UpdateCustomerContactCommand, Unit>
    {
        private readonly IMapper mapper;
        private readonly IAsyncRepository<Domain.Sales.Customer> customerRepository;
        private readonly IAsyncRepository<Domain.Person.ContactType> contactTypeRepository;
        private readonly IAsyncRepository<Person> personRepository;
        private readonly IAsyncRepository<EmailAddress> emailAddressRepository;

        public UpdateCustomerContactCommandHandler(
            IMapper mapper,
            IAsyncRepository<Domain.Person.ContactType> contactTypeRepository,
            IAsyncRepository<Domain.Sales.Customer> customerRepository,
            IAsyncRepository<Person> personRepository,
            IAsyncRepository<EmailAddress> emailAddressRepository
        ) => (this.mapper, this.contactTypeRepository, this.customerRepository, this.personRepository, this.emailAddressRepository) =
                (mapper, contactTypeRepository, customerRepository, personRepository, emailAddressRepository);

        public async Task<Unit> Handle(UpdateCustomerContactCommand request, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.FirstOrDefaultAsync(
                new GetCustomerSpecification(request.AccountNumber)
            );

            var contactType = await contactTypeRepository.FirstOrDefaultAsync(
                new GetContactTypeSpecification(request.CustomerContact.ContactTypeName)
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

        private async Task<int?> IsExistingPerson(ContactDto contact)
        {
            var person = await personRepository.FirstOrDefaultAsync(
                new GetPersonSpecification(
                    contact.FirstName,
                    contact.MiddleName,
                    contact.LastName
                )
            );

            if (person != null)
                return person.Id;

            return null;
        }

        private async Task UpdateEmailAddresses(UpdateCustomerContactCommand request)
        {
            var person = await personRepository.FirstOrDefaultAsync(
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