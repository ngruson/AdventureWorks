using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Customer.Handlers.GetPreferredAddress;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Customer.Handlers
{
    public class GetPreferredAddressQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_WithCustomerNumberAddressType_AddressReturned(
            [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
            GetPreferredAddressQueryHandler sut,
            GetPreferredAddressQuery query,
            Address address
        )
        {
            //Arrange
            mockCustomerApiClient.Setup(_ => _.GetPreferredAddressAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>()
                )
            )
            .ReturnsAsync(address);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().Be(address);

            mockCustomerApiClient.Verify(x => x.GetPreferredAddressAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>()
                )
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_WithoutCustomerNumber_ThrowsArgumentException(
            [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
            GetPreferredAddressQueryHandler sut,
            GetPreferredAddressQuery query
        )
        {
            //Arrange
            query.AccountNumber = "";

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Required input request.AccountNumber was empty. (Parameter 'request.AccountNumber')");

            mockCustomerApiClient.Verify(_ => _.GetPreferredAddressAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>()
                ),
                Times.Never
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_WithoutAddressType_ThrowsArgumentException(
            [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
            GetPreferredAddressQueryHandler sut,
            GetPreferredAddressQuery query
        )
        {
            //Arrange
            query.AddressType = "";

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Required input request.AddressType was empty. (Parameter 'request.AddressType')");

            mockCustomerApiClient.Verify(_ => _.GetPreferredAddressAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>()
                ),
                Times.Never
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_ReturnedAddressNull_ThrowsArgumentNullException(
            [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
            GetPreferredAddressQueryHandler sut,
            GetPreferredAddressQuery query
        )
        {
            //Arrange

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Value cannot be null. (Parameter 'address')");

            mockCustomerApiClient.Verify(x => x.GetPreferredAddressAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>()
                )
            );
        }
    }
}