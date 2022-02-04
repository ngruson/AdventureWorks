using AutoFixture.Xunit2;
using AW.Services.Customer.Core.Handlers.DeleteIndividualCustomerEmailAddress;
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
    public class DeleteIndividualCustomerEmailAddressCommandUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Handle_ExistingCustomer_DeleteEmailAddress(
            [Frozen] Mock<IRepository<Entities.IndividualCustomer>> customerRepoMock,
            Entities.IndividualCustomer customer,
            DeleteIndividualCustomerEmailAddressCommandHandler sut,
            DeleteIndividualCustomerEmailAddressCommand command
        )
        {
            //Arrange
            customer.Person.EmailAddresses = new List<Entities.PersonEmailAddress>
            {
                new Entities.PersonEmailAddress
                {
                    EmailAddress = command.EmailAddress
                }
            };

            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetIndividualCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(customer);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            customerRepoMock.Verify(x => x.GetBySpecAsync(
                It.IsAny<GetIndividualCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ));
            customerRepoMock.Verify(x => x.UpdateAsync(
                It.IsAny<Entities.IndividualCustomer>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory]
        [AutoMoqData]
        public void Handle_CustomerDoesNotExist_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.IndividualCustomer>> customerRepoMock,
            DeleteIndividualCustomerEmailAddressCommandHandler sut,
            DeleteIndividualCustomerEmailAddressCommand command
        )
        {
            // Arrange
            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetIndividualCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.IndividualCustomer)null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'individualCustomer')");
        }

        [Theory]
        [AutoMoqData]
        public void Handle_EmailAddressDoesNotExist_ThrowArgumentNullException(
            DeleteIndividualCustomerEmailAddressCommandHandler sut,
            DeleteIndividualCustomerEmailAddressCommand command
        )
        {
            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'emailAddress')");
        }
    }
}