using AW.Services.Customer.Core.Handlers.DeleteIndividualCustomerPhone;
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
    public class DeleteIndividualCustomerPhoneCommandUnitTests
    {
        [Fact]
        public async void Handle_ExistingCustomerAndAddress_DeleteCustomerAddress()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<DeleteIndividualCustomerPhoneCommandHandler>>();
            var customerRepoMock = new Mock<IRepository<Entities.IndividualCustomer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetIndividualCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new IndividualCustomerBuilder()
                .WithTestValues()
                .Build()
            );

            var handler = new DeleteIndividualCustomerPhoneCommandHandler(
                loggerMock.Object,
                customerRepoMock.Object
            );

            //Act
            var command = new DeleteIndividualCustomerPhoneCommand
            {
                AccountNumber = "AW00011000",
                Phone = new PhoneDto
                {
                    PhoneNumberType = "Cell",
                    PhoneNumber = "398-555-0132"
                }
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
            var loggerMock = new Mock<ILogger<DeleteIndividualCustomerPhoneCommandHandler>>();
            var customerRepoMock = new Mock<IRepository<Entities.IndividualCustomer>>();

            var handler = new DeleteIndividualCustomerPhoneCommandHandler(
                loggerMock.Object,
                customerRepoMock.Object
            );

            //Act
            var command = new DeleteIndividualCustomerPhoneCommand
            {
                AccountNumber = "AW00011000",
                Phone = new PhoneDto
                {
                    PhoneNumberType = "Cell",
                    PhoneNumber = "398-555-0132"
                }
            };
            Func<Task> func = async () => await handler.Handle(command, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'individualCustomer')");
        }

        [Fact]
        public void Handle_PhoneNumberDoesNotExist_ThrowArgumentNullException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<DeleteIndividualCustomerPhoneCommandHandler>>();
            var customerRepoMock = new Mock<IRepository<Entities.IndividualCustomer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetIndividualCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new IndividualCustomerBuilder()
                .WithTestValues()
                .Person(new PersonBuilder()
                    .WithTestValues()
                    .PhoneNumbers(new List<Entities.PersonPhone>())
                    .Build()
                )
                .Build()
            );

            var handler = new DeleteIndividualCustomerPhoneCommandHandler(
                loggerMock.Object,
                customerRepoMock.Object
            );

            //Act
            var command = new DeleteIndividualCustomerPhoneCommand
            {
                AccountNumber = "AW00011000",
                Phone = new PhoneDto
                {
                    PhoneNumberType = "Cell",
                    PhoneNumber = "398-555-0132"
                }
            };
            Func<Task> func = async () => await handler.Handle(command, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'phone')");
        }
    }
}