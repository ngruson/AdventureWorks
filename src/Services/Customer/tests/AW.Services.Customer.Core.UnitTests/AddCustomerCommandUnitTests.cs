using AutoFixture.Xunit2;
using AW.Services.Customer.Core.Handlers.AddCustomer;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests
{
    public class AddCustomerCommandUnitTests
    {
        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_NewCustomer_ReturnCustomer(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            AddCustomerCommandHandler sut,
            StoreCustomerDto customer
        )
        {
            // Arrange

            //Act
            var result = await sut.Handle(
                new AddCustomerCommand { Customer = customer }, 
                CancellationToken.None
            );

            //Assert
            result.Should().NotBeNull();
            customerRepoMock.Verify(x => x.AddAsync(
                It.IsAny<Entities.Customer>(),
                It.IsAny<CancellationToken>()
            ));

            result.Should().BeEquivalentTo(customer);
        }
    }
}