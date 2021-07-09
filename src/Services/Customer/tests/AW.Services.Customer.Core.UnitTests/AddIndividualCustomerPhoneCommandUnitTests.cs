using AW.Services.Customer.Core.Handlers.AddIndividualCustomerPhone;
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
    public class AddIndividualCustomerPhoneCommandUnitTests
    {
        [Fact]
        public async void Handle_CustomerExist_AddIndividualCustomerPhone()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<AddIndividualCustomerPhoneCommandHandler>>();
            var customerRepoMock = new Mock<IRepository<Entities.IndividualCustomer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetIndividualCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new IndividualCustomerBuilder()
                .WithTestValues()
                .Build()
            );

            var handler = new AddIndividualCustomerPhoneCommandHandler(
                loggerMock.Object,
                mapper,
                customerRepoMock.Object
            );

            //Act
            var command = new AddIndividualCustomerPhoneCommand
            {
                AccountNumber = "AW00029484",
                Phone = new PhoneDto {
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
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<AddIndividualCustomerPhoneCommandHandler>>();
            var customerRepoMock = new Mock<IRepository<Entities.IndividualCustomer>>();

            var handler = new AddIndividualCustomerPhoneCommandHandler(
                loggerMock.Object,
                mapper,
                customerRepoMock.Object
            );

            //Act
            var command = new AddIndividualCustomerPhoneCommand
            {
                AccountNumber = "AW00029484",
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
    }
}
