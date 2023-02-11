using Ardalis.Specification;
using AutoFixture.Xunit2;
using AW.Services.Customer.Core.AutoMapper;
using AW.Services.Customer.Core.Exceptions;
using AW.Services.Customer.Core.Handlers.GetAllCustomers;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class GetAllCustomersQueryUnitTests
    {
        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_CustomersExists_ReturnCustomers(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            GetAllCustomersQueryHandler sut,
            List<Entities.IndividualCustomer> customers,
            GetAllCustomersQuery query
        )
        {
            // Arrange
            customerRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetAllCustomersSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(customers.Cast<Entities.Customer>().ToList());

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            customerRepoMock.Verify(x => x.ListAsync(
                It.IsAny<ISpecification<Entities.Customer>>(),
                It.IsAny<CancellationToken>()
            ));
            result.Count.Should().Be(customers.Count);
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_NoCustomersExists_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            GetAllCustomersQueryHandler sut,
            GetAllCustomersQuery query
        )
        {
            // Arrange            
            customerRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetAllCustomersSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new List<Entities.Customer>());

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<CustomersNotFoundException>()
                .WithMessage("Customers not found");
        }
    }
}