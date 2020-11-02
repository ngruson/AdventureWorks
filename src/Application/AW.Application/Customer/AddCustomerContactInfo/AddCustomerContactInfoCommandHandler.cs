using AW.Application.Interfaces;
using AW.Application.Specifications;
using AW.Domain.Person;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.Customer.AddCustomerContactInfo
{
    public class AddCustomerContactInfoCommandHandler : IRequestHandler<AddCustomerContactInfoCommand, Unit>
    {
        private readonly IAsyncRepository<Domain.Sales.Customer> customerRepository;
        private readonly IAsyncRepository<PhoneNumberType> phoneNumberRepository;

        public AddCustomerContactInfoCommandHandler(
            IAsyncRepository<Domain.Sales.Customer> customerRepository,
            IAsyncRepository<PhoneNumberType> phoneNumberRepository
        )
        => (this.customerRepository, this.phoneNumberRepository) =
            (customerRepository, phoneNumberRepository);

        public async Task<Unit> Handle(AddCustomerContactInfoCommand request, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.FirstOrDefaultAsync(
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
                var phoneNumberType = await phoneNumberRepository.FirstOrDefaultAsync(
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