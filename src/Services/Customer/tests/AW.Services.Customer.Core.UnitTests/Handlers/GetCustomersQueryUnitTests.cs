using Ardalis.Result;
using Ardalis.Specification;
using AutoFixture.Xunit2;
using AW.Services.Customer.Core.AutoMapper;
using AW.Services.Customer.Core.Handlers.GetCustomers;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class GetCustomersQueryUnitTests
    {
        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task return_success_given_customers_exists(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            GetCustomersQueryHandler sut,
            List<Entities.IndividualCustomer> customers,
            GetCustomersQuery query
        )
        {
            // Arrange
            customerRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetCustomersSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(customers.Cast<Entities.Customer>().ToList());

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();

            customerRepoMock.Verify(x => x.ListAsync(
                It.IsAny<ISpecification<Entities.Customer>>(),
                It.IsAny<CancellationToken>()
            ));
            result.Value.Count.Should().Be(customers.Count);
        }

        [Theory]
        [AutoMoqData]
        public async Task return_notfound_given_customers_does_not_exist(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            GetCustomersQueryHandler sut,
            GetCustomersQuery query
        )
        {
            // Arrange            
            customerRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetCustomersSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new List<Entities.Customer>());

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.NotFound);
        }

        [Theory]
        [AutoMoqData]
        public async Task return_error_given_exception_was_thrown(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            GetCustomersQueryHandler sut,
            GetCustomersQuery query
        )
        {
            // Arrange
            customerRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetCustomersSpecification>(),
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
