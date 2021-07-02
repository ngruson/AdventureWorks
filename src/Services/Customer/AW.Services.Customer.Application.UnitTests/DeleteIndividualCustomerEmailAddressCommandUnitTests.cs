using Ardalis.Specification;
using AW.Services.Customer.Application.DeleteIndividualCustomerEmailAddress;
using AW.Services.Customer.Application.Specifications;
using AW.Services.Customer.Application.UnitTests.TestBuilders;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Customer.Application.UnitTests
{
    public class DeleteIndividualCustomerEmailAddressCommandUnitTests
    {
        [Fact]
        public async void Handle_ExistingCustomer_DeleteEmailAddress()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<DeleteIndividualCustomerEmailAddressCommandHandler>>();
            var customerRepoMock = new Mock<IRepositoryBase<Domain.IndividualCustomer>>();
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
                It.IsAny<Domain.IndividualCustomer>(),
                It.IsAny<CancellationToken>()    
            ));
        }

        [Fact]
        public void Handle_CustomerDoesNotExist_ThrowArgumentNullException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<DeleteIndividualCustomerEmailAddressCommandHandler>>();
            var customerRepoMock = new Mock<IRepositoryBase<Domain.IndividualCustomer>>();

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
            var customerRepoMock = new Mock<IRepositoryBase<Domain.IndividualCustomer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetIndividualCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new IndividualCustomerBuilder()
                .WithTestValues()
                .Person(new PersonBuilder()
                    .WithTestValues()
                    .EmailAddresses(new List<Domain.PersonEmailAddress>())
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