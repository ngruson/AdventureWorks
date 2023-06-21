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
        public async Task ok_given_updated_customer(
            [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
            UpdateCustomerCommandHandler sut,
            StoreCustomer customer
        )
        {
            //Arrange
            var command = new UpdateCustomerCommand(customer);

            mockCustomerApiClient.Setup(_ => _.UpdateCustomerAsync(
                    It.IsAny<Infrastructure.Api.Customer.Handlers.UpdateCustomer.Customer>()
                )
            )
            .ReturnsAsync(customer);

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            mockCustomerApiClient.Verify(x => x.UpdateCustomerAsync(
                    It.IsAny<Infrastructure.Api.Customer.Handlers.UpdateCustomer.Customer>()
                )
            );
        }

        [Theory, AutoMoqData]
        public async Task throw_argumentnullexception_given_customer_not_found(
            [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
            UpdateCustomerCommandHandler sut,
            StoreCustomer customer
        )
        {
            //Arrange
            var command = new UpdateCustomerCommand(customer);

            mockCustomerApiClient.Setup(_ => _.UpdateCustomerAsync(
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
                    It.IsAny<Infrastructure.Api.Customer.Handlers.UpdateCustomer.Customer>()
                )
            );
        }
    }
}
