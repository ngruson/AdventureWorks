using AW.Services.Customer.Core.Handlers.AddIndividualCustomerEmailAddress;
using AW.Services.Customer.Core.Specifications;
using AW.Services.Customer.Core.UnitTests.TestBuilders;
using AW.SharedKernel.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests
{
    public class AddIndividualCustomerEmailAddressCommandUnitTests
    {
        [Fact]
        public async void Handle_CustomerExist_AddIndividualCustomerEmailAddress()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<AddIndividualCustomerEmailAddressCommandHandler>>();
            var customerRepoMock = new Mock<IRepository<Entities.IndividualCustomer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetIndividualCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new IndividualCustomerBuilder()
                .WithTestValues()
                .Build()
            );

            var handler = new AddIndividualCustomerEmailAddressCommandHandler(
                loggerMock.Object,
                customerRepoMock.Object
            );

            //Act
            var command = new AddIndividualCustomerEmailAddressCommand
            {
                AccountNumber = "AW00011000",
                EmailAddress = "jon24@adventure-works.com"
            };
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            customerRepoMock.Verify(x => x.UpdateAsync(
                It.IsAny<Entities.IndividualCustomer>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Fact]
        public void Handle_CustomerDoesNotExist_ThrowArgumentNullException()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<AddIndividualCustomerEmailAddressCommandHandler>>();
            var customerRepoMock = new Mock<IRepository<Entities.IndividualCustomer>>();

            var handler = new AddIndividualCustomerEmailAddressCommandHandler(
                loggerMock.Object,
                customerRepoMock.Object
            );

            //Act
            var command = new AddIndividualCustomerEmailAddressCommand
            {
                AccountNumber = "AW00011000",
                EmailAddress = "jon24@adventure-works.com"
            };
            Func<Task> func = async () => await handler.Handle(command, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'individualCustomer')");
        }
    }
}