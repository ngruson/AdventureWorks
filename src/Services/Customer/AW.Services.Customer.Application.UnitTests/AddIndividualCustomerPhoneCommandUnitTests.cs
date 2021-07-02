using Ardalis.Specification;
using AW.Services.Customer.Application.AddIndividualCustomerPhone;
using AW.Services.Customer.Application.Specifications;
using AW.Services.Customer.Application.UnitTests.TestBuilders;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Customer.Application.UnitTests
{
    public class AddIndividualCustomerPhoneCommandUnitTests
    {
        [Fact]
        public async void Handle_CustomerExist_AddIndividualCustomerPhone()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<AddIndividualCustomerPhoneCommandHandler>>();
            var customerRepoMock = new Mock<IRepositoryBase<Domain.IndividualCustomer>>();
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
                It.IsAny<Domain.IndividualCustomer>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Fact]
        public void Handle_CustomerDoesNotExist_ThrowArgumentNullException()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<AddIndividualCustomerPhoneCommandHandler>>();
            var customerRepoMock = new Mock<IRepositoryBase<Domain.IndividualCustomer>>();

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
