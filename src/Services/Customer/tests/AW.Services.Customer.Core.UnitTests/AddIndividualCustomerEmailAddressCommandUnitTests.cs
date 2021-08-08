using AutoFixture.Xunit2;
using AW.Services.Customer.Core.Handlers.AddIndividualCustomerEmailAddress;
using AW.Services.Customer.Core.Specifications;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests
{
    public class AddIndividualCustomerEmailAddressCommandUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Handle_CustomerExist_AddIndividualCustomerEmailAddress(
            [Frozen] Mock<IRepository<Entities.IndividualCustomer>> customerRepoMock,
            AddIndividualCustomerEmailAddressCommandHandler sut,
            AddIndividualCustomerEmailAddressCommand command
        )
        {
            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            customerRepoMock.Verify(x => x.UpdateAsync(
                It.IsAny<Entities.IndividualCustomer>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory]
        [AutoMoqData]
        public void Handle_CustomerDoesNotExist_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.IndividualCustomer>> customerRepoMock,
            AddIndividualCustomerEmailAddressCommandHandler sut,
            AddIndividualCustomerEmailAddressCommand command
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
    }
}