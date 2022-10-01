using Ardalis.Specification;
using AutoFixture.Xunit2;
using AW.Services.Customer.Core.Exceptions;
using AW.Services.Customer.Core.Handlers.GetCustomers;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class GetCustomersQueryUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Handle_CustomersExists_ReturnCustomers(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            GetCustomersQueryHandler sut,
            List<Entities.IndividualCustomer> customers,
            GetCustomersQuery query
        )
        {
            // Arrange
            customerRepoMock.Setup(x => x.CountAsync(
                It.IsAny<CountCustomersSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(customers.Count);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            customerRepoMock.Verify(x => x.ListAsync(
                It.IsAny<ISpecification<Entities.Customer>>(),
                It.IsAny<CancellationToken>()
            ));
            customerRepoMock.Verify(x => x.CountAsync(
                It.IsAny<ISpecification<Entities.Customer>>(),
                It.IsAny<CancellationToken>()
            ));
            result.TotalCustomers.Should().Be(customers.Count);
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_NoCustomersExists_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            GetCustomersQueryHandler sut,
            GetCustomersQuery query
        )
        {
            // Arrange            
            customerRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetCustomersPaginatedSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((List<Entities.Customer>)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<CustomersNotFoundException>()
                .WithMessage("Customers not found");
        }
    }
}