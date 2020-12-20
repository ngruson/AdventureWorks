using Ardalis.Specification;
using AW.Core.Application.Specifications;
using AW.Core.Domain.Person;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Core.Application.Customer.AddCustomerContactInfo
{
    public class AddCustomerContactInfoCommandHandler : IRequestHandler<AddCustomerContactInfoCommand, Unit>
    {
        private readonly IRepositoryBase<Domain.Sales.Customer> customerRepository;
        private readonly IRepositoryBase<PhoneNumberType> phoneNumberRepository;

        public AddCustomerContactInfoCommandHandler(
            IRepositoryBase<Domain.Sales.Customer> customerRepository,
            IRepositoryBase<PhoneNumberType> phoneNumberRepository
        )
        => (this.customerRepository, this.phoneNumberRepository) =
            (customerRepository, phoneNumberRepository);

        public async Task<Unit> Handle(AddCustomerContactInfoCommand request, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetBySpecAsync(
                new GetCustomerSpecification(request.AccountNumber)
            );            

            if (request.CustomerContactInfo.Channel == ContactInfoChannelTypeDto.Email)
            {
                customer.Person.EmailAddresses.Add(new EmailAddress
                {
                    Id = customer.Person.Id,
                    EmailAddress1 = request.CustomerContactInfo.Value,
                    ModifiedDate = DateTime.Now,
                    rowguid = Guid.NewGuid()
                });

                await customerRepository.UpdateAsync(customer);
            }
            else if (request.CustomerContactInfo.Channel == ContactInfoChannelTypeDto.Phone)
            {
                var phoneNumberType = await phoneNumberRepository.GetBySpecAsync(
                    new GetPhoneNumberTypeSpecification(request.CustomerContactInfo.ContactInfoType)
                );

                customer.Person.PhoneNumbers.Add(new PersonPhone
                {
                    Id = customer.Person.Id,
                    PhoneNumber = request.CustomerContactInfo.Value,
                    PhoneNumberTypeID = phoneNumberType.Id,
                    ModifiedDate = DateTime.Now
                });

                await customerRepository.UpdateAsync(customer);
            }

            return Unit.Value;
        }
    }
}