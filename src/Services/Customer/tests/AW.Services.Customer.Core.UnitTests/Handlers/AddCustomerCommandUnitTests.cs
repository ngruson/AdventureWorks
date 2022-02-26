using AutoFixture.Xunit2;
using AW.Services.Customer.Core.AutoMapper;
using AW.Services.Customer.Core.Handlers.AddCustomer;
using AW.Services.SharedKernel.ValueObjects;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class AddCustomerCommandUnitTests
    {
        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_NewCustomer_ReturnCustomer(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            AddCustomerCommandHandler sut,
            string name,
            string contactType,
            List<CustomerAddressDto> addresses
        )
        {
            // Arrange
            var customer = new StoreCustomerDto
            {
                Name = name,
                Addresses = addresses,
                Contacts = new List<StoreCustomerContactDto>
                {
                    new StoreCustomerContactDto
                    {
                        ContactType = contactType,
                        ContactPerson = new PersonDto
                        {
                            EmailAddresses = new List<PersonEmailAddressDto>
                            {
                                new PersonEmailAddressDto
                                {
                                    EmailAddress = EmailAddress.Create("test@test.com").Value
                                }
                            },
                            PhoneNumbers = new List<PersonPhoneDto>()
                        }
                    }
                }
            };

            //Act
            var result = await sut.Handle(
                new AddCustomerCommand { Customer = customer }, 
                CancellationToken.None
            );

            //Assert
            result.Should().NotBeNull();
            customerRepoMock.Verify(x => x.AddAsync(
                It.IsAny<Entities.Customer>(),
                It.IsAny<CancellationToken>()
            ));

            result.Should().BeEquivalentTo(customer);
        }
    }
}