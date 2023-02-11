using AutoFixture.Xunit2;
using AW.Services.Customer.Core.Exceptions;
using AW.Services.Customer.Core.Handlers.AddIndividualCustomerEmailAddress;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.Services.SharedKernel.ValueTypes;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class AddIndividualCustomerEmailAddressCommandUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Handle_CustomerExist_AddIndividualCustomerEmailAddress(
            [Frozen] Mock<IRepository<Entities.IndividualCustomer>> customerRepoMock,
            AddIndividualCustomerEmailAddressCommandHandler sut,
            Entities.Person person,
            string accountNumber
        )
        {
            //Arrange
            var command = new AddIndividualCustomerEmailAddressCommand(
                accountNumber,
                EmailAddress.Create("test@test.com").Value
            );

            //Act
            customerRepoMock.Setup(_ =>
                _.SingleOrDefaultAsync(
                    It.IsAny<GetIndividualCustomerSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(new Entities.IndividualCustomer(person));

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
        public async Task Handle_CustomerDoesNotExist_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.IndividualCustomer>> customerRepoMock,
            AddIndividualCustomerEmailAddressCommandHandler sut,
            string accountNumber
        )
        {
            // Arrange
            var command = new AddIndividualCustomerEmailAddressCommand(
                accountNumber,
                EmailAddress.Create("test@test.com").Value
            );

            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetIndividualCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.IndividualCustomer?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<CustomerNotFoundException>()
                .WithMessage($"Customer {accountNumber} not found");
        }
    }
}