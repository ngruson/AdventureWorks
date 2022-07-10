using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Customer.Handlers.GetCustomer;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Customer.Handlers
{
    public class GetCustomerQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_WithCustomerNumber_CustomerReturned(
            [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
            GetCustomerQueryHandler sut,
            GetCustomerQuery query,
            StoreCustomer customer
        )
        {
            //Arrange
            mockCustomerApiClient.Setup(_ => _.GetCustomerAsync(
                    It.IsAny<string>()
                )
            )
            .ReturnsAsync(customer);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().Be(customer);

            mockCustomerApiClient.Verify(x => x.GetCustomerAsync(
                    It.IsAny<string>()
                )
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_WithoutCustomerNumber_ThrowsArgumentException(
            [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
            GetCustomerQueryHandler sut,
            GetCustomerQuery query
        )
        {
            //Arrange
            query.AccountNumber = "";

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Required input AccountNumber was empty. (Parameter 'AccountNumber')");

            mockCustomerApiClient.Verify(x => x.GetCustomerAsync(
                    It.IsAny<string>()
                ),
                Times.Never
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_ReturnedCustomerNull_ThrowsArgumentNullException(
            [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
            GetCustomerQueryHandler sut,
            GetCustomerQuery query
        )
        {
            //Arrange

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Value cannot be null. (Parameter 'customer')");

            mockCustomerApiClient.Verify(x => x.GetCustomerAsync(
                    It.IsAny<string>()
                )
            );
        }
    }
}
