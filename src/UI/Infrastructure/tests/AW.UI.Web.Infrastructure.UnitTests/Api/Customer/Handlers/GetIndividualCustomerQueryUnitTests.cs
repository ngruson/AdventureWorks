using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetIndividualCustomer;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Customer.Handlers
{
    public class GetIndividualCustomerQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_WithCustomerNumber_CustomerReturned(
            [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
            GetIndividualCustomerQueryHandler sut,
            GetIndividualCustomerQuery query,
            IndividualCustomer customer
        )
        {
            //Arrange
            mockCustomerApiClient.Setup(_ => _.GetCustomerAsync<IndividualCustomer>(
                    It.IsAny<string>()
                )
            )
            .ReturnsAsync(customer);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().Be(customer);

            mockCustomerApiClient.Verify(x => x.GetCustomerAsync<IndividualCustomer>(
                    It.IsAny<string>()
                )
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_WithoutCustomerNumber_ThrowsArgumentException(
            [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
            GetIndividualCustomerQueryHandler sut,
            GetIndividualCustomerQuery query
        )
        {
            //Arrange
            query.AccountNumber = "";

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Required input request.AccountNumber was empty. (Parameter 'request.AccountNumber')");

            mockCustomerApiClient.Verify(x => x.GetCustomerAsync<IndividualCustomer>(
                    It.IsAny<string>()
                ),
                Times.Never
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_ReturnedCustomerNull_ThrowsArgumentNullException(
            [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
            GetIndividualCustomerQueryHandler sut,
            GetIndividualCustomerQuery query
        )
        {
            //Arrange
            mockCustomerApiClient.Setup(_ => _.GetCustomerAsync<IndividualCustomer>(
                    It.IsAny<string>()
                )
            )
            .ReturnsAsync((IndividualCustomer?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Value cannot be null. (Parameter 'customer')");

            mockCustomerApiClient.Verify(x => x.GetCustomerAsync<IndividualCustomer>(
                    It.IsAny<string>()
                )
            );
        }
    }
}
