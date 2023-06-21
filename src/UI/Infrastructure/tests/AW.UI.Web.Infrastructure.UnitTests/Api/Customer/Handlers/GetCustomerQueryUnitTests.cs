using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetCustomer;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Customer.Handlers
{
    public class GetCustomerQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task return_customer_given_customer_exists(
            [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
            GetCustomerQueryHandler sut,
            GetCustomerQuery query,
            StoreCustomer customer
        )
        {
            //Arrange
            mockCustomerApiClient.Setup(_ => _.GetCustomerAsync(
                    query.ObjectId
                )
            )
            .ReturnsAsync(customer);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().Be(customer);

            mockCustomerApiClient.Verify(x => x.GetCustomerAsync(
                    query.ObjectId
                )
            );
        }

        [Theory, AutoMoqData]
        public async Task throw_argumentexception_given_objectid_is_empty(
            [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
            GetCustomerQueryHandler sut
        )
        {
            //Arrange
            var query = new GetCustomerQuery(Guid.Empty);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Required input request.ObjectId was empty. (Parameter 'request.ObjectId')");

            mockCustomerApiClient.Verify(x => x.GetCustomerAsync(
                    query.ObjectId
                ),
                Times.Never
            );
        }

        [Theory, AutoMoqData]
        public async Task throw_argumentexception_given_customer_not_found(
            [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
            GetCustomerQueryHandler sut,
            GetCustomerQuery query
        )
        {
            //Arrange
            mockCustomerApiClient.Setup(_ => _.GetCustomerAsync(
                    query.ObjectId
                )
            )
            .ReturnsAsync((Infrastructure.Api.Customer.Handlers.GetCustomer.Customer?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Value cannot be null. (Parameter 'customer')");

            mockCustomerApiClient.Verify(x => x.GetCustomerAsync(
                    query.ObjectId
                )
            );
        }
    }
}
