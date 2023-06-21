using Ardalis.Result;
using AutoFixture.Xunit2;
using AW.Services.Customer.Core.AutoMapper;
using AW.Services.Customer.Core.Handlers.GetCustomer;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class GetCustomerQueryUnitTests
    {
        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task return_success_given_customer_exists(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            GetCustomerQueryHandler sut,
            GetCustomerQuery query,
            Entities.StoreCustomer customer
        )
        {
            //Arrange
            customerRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(customer);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();

            customerRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ));

            result.Value.Should().BeEquivalentTo(customer, opt => opt
                .Excluding(_ => _.Path.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
            );
        }

        [Theory]
        [AutoMoqData]
        public async Task return_notfound_given_customer_does_not_exist(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            GetCustomerQueryHandler sut,
            GetCustomerQuery query
        )
        {
            // Arrange
            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.Customer?)null);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.NotFound);
        }

        [Theory]
        [AutoMoqData]
        public async Task return_error_given_exception_was_thrown(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            GetCustomerQueryHandler sut,
            GetCustomerQuery query
        )
        {
            // Arrange
            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ThrowsAsync(new Exception());

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.Error);
        }
    }
}
