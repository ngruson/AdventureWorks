using AutoFixture.Xunit2;
using AW.Services.Customer.Core.AutoMapper;
using AW.Services.Customer.Core.Handlers.GetCustomer;
using AW.Services.Customer.Core.Specifications;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class GetCustomerQueryUnitTests
    {
        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_CustomerExists_ReturnCustomer(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            GetCustomerQueryHandler sut,
            GetCustomerQuery query,
            Entities.StoreCustomer customer
        )
        {
            //Arrange
            customerRepoMock.Setup(_ => _.GetBySpecAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(customer);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            customerRepoMock.Verify(x => x.GetBySpecAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ));

            result.Should().BeEquivalentTo(customer, opt => opt
                .Excluding(_ => _.SelectedMemberPath.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
            );
        }

        [Theory]
        [AutoMoqData]
        public void Handle_CustomerNotFound_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            GetCustomerQueryHandler sut,
            GetCustomerQuery query
        )
        {
            // Arrange
            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.Customer)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'customer')");
        }
    }
}