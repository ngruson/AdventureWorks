using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Customer.Handlers.UpdateCustomer;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Customer.Handlers
{
    public class UpdateCustomerCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_WithCustomerNumber_CustomerReturned(
            [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
            UpdateCustomerCommandHandler sut,
            UpdateCustomerCommand command,
            StoreCustomer customer
        )
        {
            //Arrange
            mockCustomerApiClient.Setup(_ => _.UpdateCustomerAsync(
                    It.IsAny<string>(),
                    It.IsAny<Infrastructure.Api.Customer.Handlers.UpdateCustomer.Customer>()
                )
            )
            .ReturnsAsync(customer);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().Be(customer);

            mockCustomerApiClient.Verify(x => x.UpdateCustomerAsync(
                    It.IsAny<string>(),
                    It.IsAny<Infrastructure.Api.Customer.Handlers.UpdateCustomer.Customer>()
                )
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_WithoutCustomerNumber_ThrowsArgumentException(
            [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
            UpdateCustomerCommandHandler sut,
            UpdateCustomerCommand command
        )
        {
            //Arrange
            command.AccountNumber = "";

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Required input request.AccountNumber was empty. (Parameter 'request.AccountNumber')");

            mockCustomerApiClient.Verify(x => x.UpdateCustomerAsync(
                    It.IsAny<string>(),
                    It.IsAny<Infrastructure.Api.Customer.Handlers.UpdateCustomer.Customer>()
                ),
                Times.Never
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_ReturnedCustomerNull_ThrowsArgumentNullException(
            [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
            UpdateCustomerCommandHandler sut,
            UpdateCustomerCommand command
        )
        {
            //Arrange
            mockCustomerApiClient.Setup(_ => _.UpdateCustomerAsync(
                    It.IsAny<string>(),
                    It.IsAny<Infrastructure.Api.Customer.Handlers.UpdateCustomer.Customer>()
                )
            )
            .ReturnsAsync((Infrastructure.Api.Customer.Handlers.UpdateCustomer.Customer?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Value cannot be null. (Parameter 'customer')");

            mockCustomerApiClient.Verify(x => x.UpdateCustomerAsync(
                    It.IsAny<string>(),
                    It.IsAny<Infrastructure.Api.Customer.Handlers.UpdateCustomer.Customer>()
                )
            );
        }
    }
}
