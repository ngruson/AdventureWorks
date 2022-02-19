﻿using AutoFixture.Xunit2;
using AW.Services.Customer.Core.AutoMapper;
using AW.Services.Customer.Core.Handlers.UpdateStoreCustomerContact;
using AW.Services.Customer.Core.Specifications;
using AW.SharedKernel.Interfaces;
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
            UpdateStoreCustomerContactCommand command,
            Entities.Person contactPerson
        )
        {
            //Arrange
            customer.AddContact(
                new Entities.StoreCustomerContact(
                    command.CustomerContact.ContactType,
                    contactPerson
                )
            );

            customerRepoMock.Setup(x => x.GetBySpecAsync(
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
        public void Handle_CustomerDoesNotExist_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.StoreCustomer>> customerRepoMock,
            UpdateStoreCustomerContactCommandHandler sut,
            UpdateStoreCustomerContactCommand command
        )
        {
            // Arrange
            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetStoreCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.StoreCustomer)null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'storeCustomer')");
        }

        [Theory]
        [AutoMoqData]
        public void Handle_ContactDoesNotExist_ThrowArgumentNullException(
            UpdateStoreCustomerContactCommandHandler sut,
            UpdateStoreCustomerContactCommand command
        )
        {
            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'contact')");
        }
    }
}