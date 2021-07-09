using AW.Services.Customer.Core.Handlers.DeleteIndividualCustomerEmailAddress;
using AW.Services.Customer.Core.Specifications;
using AW.Services.Customer.Core.UnitTests.TestBuilders;
using AW.SharedKernel.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests
{
    public class DeleteIndividualCustomerEmailAddressCommandUnitTests
    {
        [Fact]
        public async void Handle_ExistingCustomer_DeleteEmailAddress()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<DeleteIndividualCustomerEmailAddressCommandHandler>>();
            var customerRepoMock = new Mock<IRepository<Entities.IndividualCustomer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetIndividualCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new IndividualCustomerBuilder()
                .WithTestValues()
                .Build()
            );

            var handler = new DeleteIndividualCustomerEmailAddressCommandHandler(
                loggerMock.Object,
                customerRepoMock.Object
            );

            //Act
            var command = new DeleteIndividualCustomerEmailAddressCommand
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
            var loggerMock = new Mock<ILogger<DeleteIndividualCustomerEmailAddressCommandHandler>>();
            var customerRepoMock = new Mock<IRepository<Entities.IndividualCustomer>>();

            var handler = new DeleteIndividualCustomerEmailAddressCommandHandler(
                loggerMock.Object,
                customerRepoMock.Object
            );

            //Act
            var command = new DeleteIndividualCustomerEmailAddressCommand
            {
                AccountNumber = "AW00011000",
                EmailAddress = "jon24@adventure-works.com"
            };
            Func<Task> func = async () => await handler.Handle(command, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'individualCustomer')");
        }

        [Fact]
        public void Handle_EmailAddressDoesNotExist_ThrowArgumentNullException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<DeleteIndividualCustomerEmailAddressCommandHandler>>();
            var customerRepoMock = new Mock<IRepository<Entities.IndividualCustomer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetIndividualCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new IndividualCustomerBuilder()
                .WithTestValues()
                .Person(new PersonBuilder()
                    .WithTestValues()
                    .EmailAddresses(new List<Entities.PersonEmailAddress>())
                    .Build()
                )
                .Build()
            );

            var handler = new DeleteIndividualCustomerEmailAddressCommandHandler(
                loggerMock.Object,
                customerRepoMock.Object
            );

            //Act
            var command = new DeleteIndividualCustomerEmailAddressCommand
            {
                AccountNumber = "AW00011000",
                EmailAddress = "jon24@adventure-works.com"
            };
            Func<Task> func = async () => await handler.Handle(command, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'emailAddress')");
        }
    }
}