using Ardalis.Specification;
using AutoFixture.Xunit2;
using AW.Services.Customer.Core.AutoMapper;
using AW.Services.Customer.Core.Handlers.GetAllCustomers;
using AW.Services.Customer.Core.Specifications;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests
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
        public void Handle_NoCustomersExists_ThrowArgumentNullException(
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
            .ReturnsAsync((List<Entities.Customer>)null);

            //Act
            Func<Task> func = async() => await sut.Handle(query, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'customers')");
        }
    }
}