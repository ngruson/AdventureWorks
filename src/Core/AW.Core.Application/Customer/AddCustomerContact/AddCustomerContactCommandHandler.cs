using Ardalis.Specification;
using AutoMapper;
using AW.Core.Application.Specifications;
using AW.Core.Domain.Person;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Core.Application.Customer.AddCustomerContact
{
    public class AddCustomerContactCommandHandler : IRequestHandler<AddCustomerContactCommand, Unit>
    {
        private readonly IMapper mapper;
        private readonly IRepositoryBase<Domain.Sales.Customer> customerRepository;
        private readonly IRepositoryBase<Domain.Person.ContactType> contactTypeRepository;
        private readonly IRepositoryBase<Person> personRepository;

        public AddCustomerContactCommandHandler(
            IMapper mapper,
            IRepositoryBase<Domain.Sales.Customer> customerRepository,
            IRepositoryBase<Domain.Person.ContactType> contactTypeRepository,
            IRepositoryBase<Person> personRepository
        )
        => (this.mapper, this.customerRepository, this.contactTypeRepository, this.personRepository) = 
            (mapper, customerRepository, contactTypeRepository, personRepository);
        
        public async Task<Unit> Handle(AddCustomerContactCommand request, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetBySpecAsync(
                new GetCustomerSpecification(request.AccountNumber)
            );

            var contactType = await contactTypeRepository.GetBySpecAsync(
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
            var person = await personRepository.GetBySpecAsync(
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
            if (request.CustomerContact.Contact.EmailAddresses.Count > 0)
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
}