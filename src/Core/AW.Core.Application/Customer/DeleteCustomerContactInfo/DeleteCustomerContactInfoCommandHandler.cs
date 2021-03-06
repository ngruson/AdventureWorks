﻿using Ardalis.Specification;
using AW.Core.Application.Specifications;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Core.Application.Customer.DeleteCustomerContactInfo
{
    public class DeleteCustomerContactInfoCommandHandler : IRequestHandler<DeleteCustomerContactInfoCommand, Unit>
    {
        private readonly IRepositoryBase<Domain.Sales.Customer> customerRepository;

        public DeleteCustomerContactInfoCommandHandler(IRepositoryBase<Domain.Sales.Customer> customerRepository) =>
            this.customerRepository = customerRepository;

        public async Task<Unit> Handle(DeleteCustomerContactInfoCommand request, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetBySpecAsync(
                new GetCustomerSpecification(request.AccountNumber)
            );

            if (request.CustomerContactInfo.Channel == ContactInfoChannelTypeDto.Email)
            {
                var emailAddress = customer.Person.EmailAddresses.FirstOrDefault(e =>
                    e.EmailAddress1 == request.CustomerContactInfo.Value
                );

                customer.Person.EmailAddresses.Remove(emailAddress);
            }
            else
            {
                var phoneNumber = customer.Person.PhoneNumbers.FirstOrDefault(p =>
                    p.PhoneNumberType.Name == request.CustomerContactInfo.ContactInfoType &&
                    p.PhoneNumber == request.CustomerContactInfo.Value
                );

                customer.Person.PhoneNumbers.Remove(phoneNumber);
            }

            await customerRepository.UpdateAsync(customer);
            return Unit.Value;
        }
    }
}