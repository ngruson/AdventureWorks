using AutoMapper;
using AW.Application.Interfaces;
using AW.Application.Specifications;
using AW.Domain.Person;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.Customer.AddCustomerContact
{
    public class AddCustomerContactCommandHandler : IRequestHandler<AddCustomerContactCommand, Unit>
    {
        private readonly IMapper mapper;
        private readonly IAsyncRepository<Domain.Sales.Customer> customerRepository;
        private readonly IAsyncRepository<Domain.Person.ContactType> contactTypeRepository;
        private readonly IAsyncRepository<Person> personRepository;
        private readonly IAsyncRepository<EmailAddress> emailAddressRepository;

        public AddCustomerContactCommandHandler(
            IMapper mapper,
            IAsyncRepository<Domain.Sales.Customer> customerRepository,
            IAsyncRepository<Domain.Person.ContactType> contactTypeRepository,
            IAsyncRepository<Person> personRepository,
            IAsyncRepository<EmailAddress> emailAddressRepository
        )
        => (this.mapper, this.customerRepository, this.contactTypeRepository, this.personRepository, this.emailAddressRepository) = 
            (mapper, customerRepository, contactTypeRepository, personRepository, emailAddressRepository);
        
        public async Task<Unit> Handle(AddCustomerContactCommand request, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.FirstOrDefaultAsync(
                new GetCustomerSpecification(request.AccountNumber)
            );

            var contactType = await contactTypeRepository.FirstOrDefaultAsync(
                new GetContactTypeSpecification(request.CustomerContact.ContactTypeName)
            );

            var customerContact = new BusinessEntityContact
            {
                ContactTypeID = contactType.Id,
                ModifiedDate = DateTime.Now,
                rowguid = Guid.NewGuid()
            };

            var existingPersonID = await IsExistingPerson(request.CustomerContact.Contact);

            if (existingPersonID.HasValue)
                customerContact.PersonID = existingPersonID.Value;
            else
            {
                customerContact.Person = mapper.Map<Person>(request.CustomerContact.Contact);
            }

            if (customer.Store != null)
                customer.Store.BusinessEntityContacts.Add(customerContact);
            else if (customer.Person != null)
                customer.Person.BusinessEntityContacts.Add(customerContact);

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

        private async Task UpdateEmailAddresses(AddCustomerContactCommand request)
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