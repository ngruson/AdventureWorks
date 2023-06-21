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
        public async Task return_customer_given_customer_found(
            [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
            GetIndividualCustomerQueryHandler sut,
            GetIndividualCustomerQuery query,
            IndividualCustomer customer
        )
        {
            //Arrange
            mockCustomerApiClient.Setup(_ => _.GetCustomerAsync<IndividualCustomer>(
                    query.ObjectId
                )
            )
            .ReturnsAsync(customer);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().Be(customer);

            mockCustomerApiClient.Verify(x => x.GetCustomerAsync<IndividualCustomer>(
                    query.ObjectId
                )
            );
        }

        [Theory, AutoMoqData]
        public async Task throw_argumentexception_given_empty_objectid(
            [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
            GetIndividualCustomerQueryHandler sut
        )
        {
            //Arrange
            var query = new GetIndividualCustomerQuery(Guid.Empty);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Required input request.ObjectId was empty. (Parameter 'request.ObjectId')");

            mockCustomerApiClient.Verify(x => x.GetCustomerAsync<IndividualCustomer>(
                    query.ObjectId
                ),
                Times.Never
            );
        }

        [Theory, AutoMoqData]
        public async Task throw_argumentnullexception_given_customer_not_found(
            [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
            GetIndividualCustomerQueryHandler sut,
            GetIndividualCustomerQuery query
        )
        {
            //Arrange
            mockCustomerApiClient.Setup(_ => _.GetCustomerAsync<IndividualCustomer>(
                    query.ObjectId
                )
            )
            .ReturnsAsync((IndividualCustomer?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Value cannot be null. (Parameter 'customer')");

            mockCustomerApiClient.Verify(x => x.GetCustomerAsync<IndividualCustomer>(
                    query.ObjectId
                )
            );
        }
    }
}
