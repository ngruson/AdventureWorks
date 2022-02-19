using AutoFixture.Xunit2;
using AW.Services.Customer.Core.AutoMapper;
using AW.Services.Customer.Core.Handlers.AddStoreCustomerContact;
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
    public class AddStoreCustomerContactCommandUnitTests
    {
        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_CustomerExist_AddStoreCustomerContact(
            [Frozen] Mock<IRepository<Entities.StoreCustomer>> customerRepoMock,
            AddStoreCustomerContactCommandHandler sut,
            AddStoreCustomerContactCommand command
        )
        {
            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            customerRepoMock.Verify(x => x.UpdateAsync(
                It.IsAny<Entities.StoreCustomer>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory]
        [AutoMoqData]
        public void Handle_CustomerDoesNotExist_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.StoreCustomer>> customerRepoMock,
            AddStoreCustomerContactCommandHandler sut,
            AddStoreCustomerContactCommand command
        )
        {
            // Arrange
            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetStoreCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.StoreCustomer)null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'storeCustomer')");
        }
    }
}