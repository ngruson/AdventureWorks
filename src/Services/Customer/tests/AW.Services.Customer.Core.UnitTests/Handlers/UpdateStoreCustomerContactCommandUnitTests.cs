using AutoFixture.Xunit2;
using AW.Services.Customer.Core.AutoMapper;
using AW.Services.Customer.Core.Handlers.UpdateStoreCustomerContact;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.Services.SharedKernel.ValueTypes;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class UpdateStoreCustomerContactCommandUnitTests
    {
        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_CustomerAndContactExist_UpdateStoreCustomerContact(
            [Frozen] Mock<IRepository<Entities.StoreCustomer>> customerRepoMock,
            Entities.StoreCustomer customer,
            UpdateStoreCustomerContactCommandHandler sut,
            Entities.Person contactPerson,
            string accountNumber,
            string contactType
        )
        {
            //Arrange
            var command = new UpdateStoreCustomerContactCommand
            {
                AccountNumber = accountNumber,
                CustomerContact = new StoreCustomerContactDto
                {
                    ContactType = contactType,
                    ContactPerson = new PersonDto
                    {
                        EmailAddresses = new List<EmailAddressDto>
                        {
                            new EmailAddressDto
                            {
                                EmailAddress = EmailAddress.Create("test@test.com").Value
                            }
                        }
                    }
                }
            };

            customer.AddContact(
                new Entities.StoreCustomerContact(
                    command.CustomerContact.ContactType,
                    contactPerson
                )
            );

            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetStoreCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(customer);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            customerRepoMock.Verify(x => x.UpdateAsync(
                It.IsAny<Entities.StoreCustomer>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_CustomerDoesNotExist_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.StoreCustomer>> customerRepoMock,
            UpdateStoreCustomerContactCommandHandler sut,
            string accountNumber,
            string contactType
        )
        {
            // Arrange
            var command = new UpdateStoreCustomerContactCommand
            {
                AccountNumber = accountNumber,
                CustomerContact = new StoreCustomerContactDto
                {
                    ContactType = contactType,
                    ContactPerson = new PersonDto
                    {
                        EmailAddresses = new List<EmailAddressDto>()
                    }
                }
            };

            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetStoreCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.StoreCustomer)null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'storeCustomer')");
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_ContactDoesNotExist_ThrowArgumentNullException(
            UpdateStoreCustomerContactCommandHandler sut,
            string accountNumber,
            string contactType
        )
        {
            //Arrange
            var command = new UpdateStoreCustomerContactCommand
            {
                AccountNumber = accountNumber,
                CustomerContact = new StoreCustomerContactDto
                {
                    ContactType = contactType,
                    ContactPerson = new PersonDto
                    {
                        EmailAddresses = new List<EmailAddressDto>()
                    }
                }
            };

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'contact')");
        }
    }
}